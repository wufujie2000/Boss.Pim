using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Threading;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.DomainServices;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Dapper;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class PeriodIncreaseAppService : AsyncCrudAppService<PeriodIncrease, PeriodIncreaseDto>, IPeriodIncreaseAppService
    {
        public ICacheManager CacheManager { get; set; }
        public FundDomainService FundDomainService { get; set; }
        public WebSrcUtil WebSrcUtil { get; set; }

        public PeriodIncreaseAppService(IRepository<PeriodIncrease> repository) : base(repository)
        {
        }

        public async Task AsyncDownoad()
        {
            int size = 50;
            int page = 1;

            while (true)
            {
                var isLast = await DownloadByPager(page, size);
                if (isLast)
                {
                    break;
                }
                page++;
            }

        }

        private async Task<bool> DownloadByPager(int page, int size)
        {
            bool isLast = false;
            Logger.Info($"开始下载 PeriodIncrease 第{page}页，每页{size}条");
            using (var conn = new SqlConnection(SQLUtil.DefaultConnStr))
            {
                conn.Execute(@"
DELETE dbo.FundCenter_PeriodIncreases
WHERE Id IN
      (
          SELECT MAX(CONVERT(VARCHAR(64), Id))
          FROM dbo.FundCenter_PeriodIncreases
          GROUP BY Title,
                   FundCode,
                   ClosingDate
          HAVING COUNT(1) > 1
      );
            ");
                var funds = FundDomainService.GetQuery().OrderBy(a => a.Id).PageIndex(page, size).Select(a => a.Code).ToList();
                if (funds.Count < size)
                {
                    isLast = true;
                }
                if (funds.Count > 0)
                {
                    await Insert(funds);
                }
            }
            Logger.Info($"PeriodIncrease 下载完成 第{page}页，每页{size}条");
            return isLast;
        }

        private async Task Insert(ICollection<string> funds)
        {
            List<PeriodIncrease> modellist = new List<PeriodIncrease>();
            foreach (var fund in funds.Distinct().ToList())
            {
                if (string.IsNullOrWhiteSpace(fund))
                {
                    continue;
                }
                try
                {
                    var itemmodellist = await Download(fund);
                    if (itemmodellist != null)
                    {
                        modellist.AddRange(itemmodellist);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(fund + " " + e.Message, e);
                }
            }
            if (modellist.Count > 0)
            {
                await CheckAndInsert(modellist);
            }
        }

        public async Task DownoadByFundCode(string fundCodes)
        {
            var funds = fundCodes.ToStringArray();
            await Insert(funds);
        }

        private async Task<List<PeriodIncrease>> Download(string fundCode, string range = "")
        {
            string url = $"https://fundmobapi.eastmoney.com/FundMApi/FundPeriodIncrease.ashx?FCODE={fundCode}&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&RANGE={range}";
            var str = await WebSrcUtil.GetToString(url);
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            var data = JsonConvert.DeserializeObject<EastmoneyResponse<PeriodIncreaseData, PeriodIncreaseExpansion>>(str);
            if (data?.Datas == null || data.Datas.Length <= 0 || data.Expansion == null)
            {
                return null;
            }
            List<PeriodIncrease> modellist = new List<PeriodIncrease>();
            foreach (var item in data.Datas)
            {
                var closingDate = data.Expansion.TIME?.TryToDateTimeOrNull();
                if (closingDate == null)
                {
                    continue;
                }
                modellist.Add(new PeriodIncrease
                {
                    FundCode = fundCode,
                    ClosingDate = closingDate.Value,

                    Title = item.title,
                    ReturnRate = item.syl.TryToFloat(-1),
                    Hs300 = item.hs300.TryToFloat(-1),
                    SameTypeAverage = item.avg.TryToFloat(-1),

                    Rank = item.rank.TryToInt(-1),
                    SameTypeTotalQty = item.sc.TryToInt(-1),

                    DifferentQty = item.diff.TryToInt(-1),
                });
            }
            return modellist;
        }

        private async Task CheckAndInsert(List<PeriodIncrease> list)
        {
            int size = 100;
            int page = 1;
            while (true)
            {
                var execList = list.Skip((page - 1) * size).Take(size).ToList();
                if (execList.Count <= 0)
                {
                    break;
                }
                using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
                {
                    foreach (var item in execList)
                    {
                        if (Repository.GetAll().Any(a => a.FundCode == item.FundCode && a.Title == item.Title && a.ClosingDate == item.ClosingDate))
                        {
                            continue;
                        }
                        await Repository.InsertAsync(item);
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }



        public async Task DownloadRank()
        {
            Logger.Info("开始下载 更新 FundRank");
            int size = 50;
            int page = 1;
            while (true)
            {
                var url = $"https://fundmobapi.eastmoney.com/FundMApi/FundRankNewList.ashx?fundtype=0&SortColumn=RZDF&Sort=desc&pageIndex={page}&pagesize={size}&companyid=&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0";
                var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(str))
                {
                    Logger.Info("更新 FundRank 完成");
                    return;
                }
                var data = JsonConvert.DeserializeObject<EastmoneyResponse<FundRankDatas>>(str);
                if (data?.Datas == null || data.Datas.Length <= 0)
                {
                    Logger.Info("更新 FundRank 完成");
                    return;
                }

                await InsertFundRank(data);
                page++;
            }
        }

        private async Task InsertFundRank(EastmoneyResponse<FundRankDatas> data)
        {
            List<FundRank> result = new List<FundRank>();
            foreach (var item in data.Datas)
            {
                result.Add(new FundRank
                {
                    FundCode = item.FCODE,
                    UnitNetWorth = item.DWJZ.TryToFloat(-1),
                    DailyGrowthRate = item.RZDF.TryToFloat(-1),
                    Date = item.FSRQ.TryToDateTime(),

                    ZGrowthRate = item.SYL_Z.TryToFloat(-1),
                    YGrowthRate = item.SYL_Y.TryToFloat(-1),
                    Y3GrowthRate = item.SYL_3Y.TryToFloat(-1),
                    Y6GrowthRate = item.SYL_6Y.TryToFloat(-1),
                    N1GrowthRate = item.SYL_1N.TryToFloat(-1),
                    N2GrowthRate = item.SYL_2N.TryToFloat(-1),
                    N3GrowthRate = item.SYL_3N.TryToFloat(-1),
                    N5GrowthRate = item.SYL_5N.TryToFloat(-1),

                    JNGrowthRate = item.SYL_JN.TryToFloat(-1),
                    LNGrowthRate = item.SYL_LN.TryToFloat(-1),
                });
            }
            await FundDomainService.CheckInsertFundRank(result);
        }
    }
}