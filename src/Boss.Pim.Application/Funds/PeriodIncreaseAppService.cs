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
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Threading;
using Boss.Pim.AdoNet;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Jobs;
using Boss.Pim.Funds.Services;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Dapper;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class PeriodIncreaseAppService : AsyncCrudAppService<PeriodIncrease, PeriodIncreaseDto>, IPeriodIncreaseAppService
    {
        public BackgroundJobManager BackgroundJobManager { get; set; }
        public FundManager FundDomainService { get; set; }
        public WebSrcUtil WebSrcUtil { get; set; }
        public PeriodIncreaseManager PeriodIncreaseManager { get; set; }

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
            var today = DateTime.Now.Date;
            var notquery = Repository.GetAll()
                .Where(a => a.Title == "Z" && (a.CreationTime == today || a.LastModificationTime == today))
                .Select(a => a.FundCode).Distinct();
            var funds = FundDomainService.GetQuery()
                .Where(a => !notquery.Contains(a.Code))
                .OrderBy(a => a.Id).PageIndex(page, size).Select(a => a.Code).ToList();
            if (funds.Count < size)
            {
                isLast = true;
            }
            if (funds.Count > 0)
            {
                //await BackgroundJobManager.EnqueueAsync<InsertPeriodIncreaseJob, ICollection<string>>(funds);
                await PeriodIncreaseManager.Insert(funds);
            }
            Logger.Info($"PeriodIncrease 下载完成 第{page}页，每页{size}条");
            return isLast;
        }



        public async Task DownoadByFundCode(string fundCodes)
        {
            var funds = fundCodes.ToStringArray();
            await PeriodIncreaseManager.Insert(funds);
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