using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Boss.Pim.Extensions;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds.Services
{
    public class PeriodIncreaseManager : PimDomainServiceBase, ISingletonDependency
    {
        public IRepository<PeriodIncrease> PeriodIncreaseRepository { get; set; }
        public WebSrcUtil WebSrcUtil { get; set; }

        public async Task Insert(ICollection<string> funds)
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
                        //if (PeriodIncreaseRepository.GetAll().Any(a => a.FundCode == item.FundCode && a.Title == item.Title && (a.CreationTime == item.CreationTime || a.LastModificationTime == item.LastModificationTime)))
                        //{
                        //    continue;
                        //}
                        await PeriodIncreaseRepository.InsertAsync(item);
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }
    }
}
