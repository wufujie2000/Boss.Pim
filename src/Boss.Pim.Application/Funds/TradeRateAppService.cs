using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.DomainServices;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class TradeRateAppService : AsyncCrudAppService<TradeRate, TradeRateDto>, ITradeRateAppService
    {
        public WebSrcUtil WebSrcUtil { get; set; }
        public IRepository<TradeRecord, Guid> TradeRecordRepository { get; set; }
        public FundDomainService FundDomainService { get; set; }

        public TradeRateAppService(IRepository<TradeRate, int> repository) : base(repository)
        {
        }

        public async Task DownloadTrade()
        {
            var funds = TradeRecordRepository.GetAll().Select(a => a.FundCode).Distinct().ToList();
            foreach (var fund in funds)
            {
                await DownloadByFuncode(fund);
            }
        }

        public async Task DownloadByFuncode(string fund)
        {
            try
            {
                var modellist = await Download(fund);
                if (modellist.Count > 0)
                {
                    await CheckAndInsert(modellist);
                }
            }
            catch (Exception e)
            {
                Logger.Error(fund + " " + e.Message, e);
            }
        }

        [UnitOfWork(false)]
        public async Task Download()
        {
            IQueryable<Fund> fundsQuery = await FundDomainService.GetFundsQuery();
            var list = await AsyncQueryableExecuter.ToListAsync(fundsQuery.Select(a => new { a.Code, a.DkhsCode }));
            foreach (var fund in list)
            {
                await DownloadByFuncode(fund.Code);
            }
        }

        [UnitOfWork(false)]
        public async Task DownloadOptional()
        {
            IQueryable<Fund> fundsQuery = await FundDomainService.GetFundsQuery(true);
            var list = await AsyncQueryableExecuter.ToListAsync(fundsQuery.Select(a => new { a.Code, a.DkhsCode }));
            foreach (var fund in list)
            {
                await DownloadByFuncode(fund.Code);
            }
        }

        [UnitOfWork(false)]
        public async Task DownloadByFundCode(List<string> fundCodes)
        {
            var list = await AsyncQueryableExecuter.ToListAsync(
                FundDomainService.GetQuery().Where(a => fundCodes.Contains(a.Code)).Select(a => new { a.Code, a.DkhsCode })
            );
            foreach (var fund in list)
            {
                await DownloadByFuncode(fund.Code);
            }
        }

        public async Task<List<TradeRate>> Download(string fundCode)
        {
            List<TradeRate> modellist = new List<TradeRate>();
            var url = $"https://fundmobapi.eastmoney.com/FundMApi/FundRateInfo.ashx?FCODE={fundCode}&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=2686625193694544";
            var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(str))
            {
                return modellist;
            }
            var data = JsonConvert.DeserializeObject<EastmoneyFundRateInfo>(str);
            var item = data.Datas;
            //if (!string.IsNullOrWhiteSpace(item.MGREXP))
            //{
            //    modellist.Add(new TradeRate
            //    {
            //        MoneyRange = -1,
            //        DayRange = 365,
            //        FundCode = fundCode,
            //        Rate = item.MGREXP.Replace("%", "").TryToFloat(-1),
            //        SourceRate = item.MGREXP.Replace("%", "").TryToFloat(-1),
            //        Title = "管理费(每年)",
            //        RateType = ObjectValues.TradeRateType.管理费
            //    });
            //}
            //if (!string.IsNullOrWhiteSpace(item.SALESEXP))
            //{
            //    modellist.Add(new TradeRate
            //    {
            //        MoneyRange = -1,
            //        DayRange = 365,
            //        FundCode = fundCode,
            //        Rate = item.SALESEXP.Replace("%", "").TryToFloat(-1),
            //        SourceRate = item.SALESEXP.Replace("%", "").TryToFloat(-1),
            //        Title = "销售服务费(每年)",
            //        RateType = ObjectValues.TradeRateType.销售服务费
            //    });
            //}
            //if (!string.IsNullOrWhiteSpace(item.TRUSTEXP))
            //{
            //    modellist.Add(new TradeRate
            //    {
            //        MoneyRange = -1,
            //        DayRange = 365,
            //        FundCode = fundCode,
            //        Rate = item.TRUSTEXP.Replace("%", "").TryToFloat(-1),
            //        SourceRate = item.TRUSTEXP.Replace("%", "").TryToFloat(-1),
            //        Title = "托管费(每年)",
            //        RateType = ObjectValues.TradeRateType.托管费
            //    });
            //}
            //if (item.rg != null && item.rg.Count > 0)
            //{
            //    foreach (var rgitem in item.rg)
            //    {
            //        var rate = rgitem.rate?.Replace("%", "").TryToFloat(-1) ?? -1;
            //        if (rate == -1)
            //        {
            //            continue;
            //        }
            //        modellist.Add(new TradeRate
            //        {
            //            MoneyRange = rgitem.money.GetNumber().TryToInt(-1),
            //            DayRange = -1,
            //            FundCode = fundCode,
            //            Rate = rate,
            //            SourceRate = rgitem?.source.Replace("%", "").TryToFloat(-1) ?? -1,
            //            Title = rgitem.money,
            //            RateType = ObjectValues.TradeRateType.认购费率
            //        });
            //    }
            //}
            if (item.sg != null && item.sg.Count > 0)
            {
                foreach (var sgitem in item.sg)
                {
                    var rate = sgitem.rate?.Replace("%", "").TryToFloat(-1) ?? -1;
                    if (rate == -1)
                    {
                        continue;
                    }
                    modellist.Add(new TradeRate
                    {
                        MoneyRange = sgitem.money.GetNumber().TryToInt(-1),
                        DayRange = -1,
                        FundCode = fundCode,
                        Rate = rate,
                        SourceRate = sgitem?.source.Replace("%", "").TryToFloat(-1) ?? -1,
                        Title = sgitem.money,
                        RateType = ObjectValues.TradeRateType.申购费率
                    });
                }
            }
            if (item.sh != null && item.sh.Count > 0)
            {
                foreach (var shitem in item.sh)
                {
                    var rate = shitem.rate?.Replace("%", "").TryToFloat(-1) ?? -1;
                    if (rate == -1)
                    {
                        continue;
                    }
                    var minDayRange = -1;
                    var maxDayRange = -1;
                    if (!string.IsNullOrWhiteSpace(shitem.time))
                    {
                        var dayuStrs = shitem.time.Split('>', '≥');
                        if (dayuStrs.Length == 2)
                        {
                            //单项大于 最小值
                            minDayRange = GetDays(shitem.time);
                        }
                        var xiaoyuStrs = shitem.time.Split('<', '≤');
                        if (xiaoyuStrs.Length == 2)
                        {
                            //单项小于 最小值
                            maxDayRange = GetDays(shitem.time);
                        }
                        else if (xiaoyuStrs.Length == 3)
                        {
                            minDayRange = GetDays(xiaoyuStrs[0]);
                            maxDayRange = GetDays(xiaoyuStrs[2]);
                        }

                        //minDayRange = GetDays(shitem.time.Substring(0, 6));
                        //var maxTxt = shitem.time.Substring(7);
                        //if (minDayRange == -1 && (maxTxt.IndexOf(">") >= 0 || maxTxt.IndexOf("≥") >= 0))
                        //{
                        //    minDayRange = GetDays(maxTxt);
                        //}
                        //else
                        //{
                        //    maxDayRange = GetDays(maxTxt);
                        //}
                    }

                    modellist.Add(new TradeRate
                    {
                        MoneyRange = -1,
                        MinDayRange = minDayRange,
                        MaxDayRange = maxDayRange,
                        FundCode = fundCode,
                        Rate = rate,
                        SourceRate = shitem?.source.Replace("%", "").TryToFloat(-1) ?? -1,
                        Title = shitem.time,
                        RateType = ObjectValues.TradeRateType.赎回费率
                    });
                }
            }
            return modellist;
        }

        private int GetDays(string txt)
        {
            int result = -1;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (txt.IndexOf('年') >= 0)
                {
                    result = (txt.GetNumber().TryToDecimal(-1) * 356).RoundToInt();
                }
                if (txt.IndexOf('月') >= 0)
                {
                    result = (txt.GetNumber().TryToDecimal(-1) * 30).RoundToInt();
                }
                if (txt.IndexOf('天') >= 0)
                {
                    result = (txt.GetNumber().TryToDecimal(-1) * 1).RoundToInt();
                }
            }
            return result;
        }

        private async Task CheckAndInsert(List<TradeRate> list)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                foreach (var item in list)
                {
                    if (Repository.GetAll().Any(a => a.FundCode == item.FundCode && a.RateType == item.RateType))
                    {
                        return;
                    }
                    await Repository.InsertAsync(item);
                }
                await uow.CompleteAsync();
            }
        }
    }
}