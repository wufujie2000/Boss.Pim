using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Boss.Pim.Extensions;
using Boss.Pim.Funds.DomainServices;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds
{
    public class ValuationAppService : AsyncCrudAppService<Valuation, ValuationDto>, IValuationAppService
    {
        public FundDomainService FundDomainService { get; set; }
        public WebSrcUtil WebSrcUtil { get; set; }

        public ValuationAppService(IRepository<Valuation> repository) : base(repository)
        {
        }
        public async Task DownloadAllFundEasyMoney()
        {
            int size = 30;
            int page = 1;
            while (true)
            {
                string url =
                    $"https://fundmobapi.eastmoney.com/FundMApi/FundValuationList.ashx?fundtype=0&SortColumn=GSZZL&Sort=desc&pageIndex={page}&pagesize={size}&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=2686625193694544";
                var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(str))
                {
                    return;
                }
                var data = JsonConvert.DeserializeObject<EastmoneyResponse<ValuationData>>(str);
                if (data?.Datas == null || data.Datas.Length <= 0)
                {
                    return;
                }
                List<Valuation> modellist = new List<Valuation>();
                foreach (var item in data.Datas)
                {
                    if (item.GSZ == null || item.GSZZL == null)
                    {
                        continue;
                    }
                    modellist.Add(new Valuation
                    {
                        FundCode = item.FCODE,
                        EstimatedTime = item.GZTIME.TryToDateTime(),
                        EstimatedUnitNetWorth = item.GSZ.Value,
                        ReturnRate = item.GSZZL.Value,

                        SourcePlatform = "eastmoney",
                    });
                }
                if (modellist.Count > 0)
                {
                    await CheckAndInsert(modellist);
                }
                if (data.Datas.Length < size)
                {
                    return;
                }
                page++;
                Thread.Sleep(1000);
            }
        }

        public async Task DownloadOptionalEasyMoney()
        {
            Logger.Info("开始下载 自选基金估值");
            var funds = await FundDomainService.GetDownloadFundCodes(true);
            foreach (var fund in funds)
            {
                await DownloadByFundCode(fund);
            }
            Logger.Info("自选基金估值 下载完成");
        }

        private async Task Insert(ICollection<string> fundCodes)
        {
            var list = await AsyncQueryableExecuter.ToListAsync(
                FundDomainService.GetQuery().Where(a => fundCodes.Contains(a.Code)).Select(a => new { a.Code, a.DkhsCode })
            );
            foreach (var fund in list)
            {
                await DownloadByFundCode(fund.Code);
            }
        }

        private async Task DownloadByFundCode(string fund)
        {
            try
            {
                string url = $"https://fundmobapi.eastmoney.com/FundMApi/FundValuationDetail.ashx?FCODE={fund}&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=2686625193694544";
                var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(str))
                {
                    return;
                }
                var data = JsonConvert.DeserializeObject<EastmoneyResponse<ValuationDetailData>>(str);
                if (data?.Datas == null || data.Datas.Length <= 0)
                {
                    return;
                }
                List<Valuation> modellist = new List<Valuation>();
                foreach (var item in data.Datas)
                {
                    if (string.IsNullOrWhiteSpace(item.dwjz) || string.IsNullOrWhiteSpace(item.gszzl) || string.IsNullOrWhiteSpace(item.gsz))
                    {
                        continue;
                    }
                    modellist.Add(new Valuation
                    {
                        FundCode = item.fundcode,
                        EstimatedTime = item.gztime.TryToDateTime(),
                        LastUnitNetWorth = item.dwjz.TryToFloat(),
                        EstimatedUnitNetWorth = item.gsz.TryToFloat(),
                        ReturnRate = item.gszzl.TryToFloat(),

                        SourcePlatform = "eastmoney",
                    });
                }
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

        public async Task DownoadByFundCode(string fundCodes)
        {
            var funds = fundCodes.ToStringArray();
            await Insert(funds);
        }

        private async Task CheckAndInsert(List<Valuation> list)
        {
            int size = 50;
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
                        var info = Repository.GetAll()
                            .FirstOrDefault(a => a.FundCode == item.FundCode && a.SourcePlatform == item.SourcePlatform);
                        if (info != null)
                        {
                            info.FundCode = item.FundCode;
                            info.EstimatedTime = item.EstimatedTime;
                            info.EstimatedUnitNetWorth = item.EstimatedUnitNetWorth;
                            info.ReturnRate = item.ReturnRate;
                            await Repository.UpdateAsync(info);
                        }
                        else
                        {
                            await Repository.InsertAsync(item);
                        }
                    }
                    await uow.CompleteAsync();
                }
                page++;
            }
        }
    }
}