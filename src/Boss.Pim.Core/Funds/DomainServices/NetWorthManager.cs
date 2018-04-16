using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Boss.Pim.Extensions;
using Boss.Pim.Sdk.Dkhs;
using Boss.Pim.Sdk.Dkhs.Responses;
using Boss.Pim.Sdk.Eastmoney;
using Boss.Pim.Sdk.Eastmoney.Responses;
using Boss.Pim.Utils;
using Newtonsoft.Json;

namespace Boss.Pim.Funds.DomainServices
{
    public class NetWorthManager : PimDomainServiceBase, ISingletonDependency
    {
        public WebSrcUtil WebSrcUtil { get; set; }
        //public async Task<List<NetWorth>> DownloadNetWorth(string fundCode, string range)
        //{
        //    List<NetWorth> modellist = new List<NetWorth>();

        //    string url = $"https://fundmobapitest.eastmoney.com/FundMApi/FundNetDiagram.ashx?FCODE={fundCode}&RANGE={range}&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0";
        //    var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
        //    if (string.IsNullOrWhiteSpace(str))
        //    {
        //        return modellist;
        //    }
        //    var data = JsonConvert.DeserializeObject<EastmoneyResponse<NetDiagramData>>(str);
        //    if (data?.Datas == null || data.Datas.Length <= 0)
        //    {
        //        return modellist;
        //    }
        //    foreach (var item in data.Datas)
        //    {
        //        var date = item.FSRQ.TryToDateTimeOrNull();
        //        var unitNetWorth = item.DWJZ.TryToFloat();
        //        var accumulatedNetWorth = item.LJJZ.TryToFloat();
        //        if (date == null || unitNetWorth == -1 || accumulatedNetWorth == -1)
        //        {
        //            continue;
        //        }
        //        modellist.Add(new NetWorth
        //        {
        //            FundCode = fundCode,

        //            Date = date.Value,
        //            UnitNetWorth = unitNetWorth,
        //            AccumulatedNetWorth = accumulatedNetWorth,
        //            DailyGrowthRate = item.JZZZL.Replace("%", "").TryToFloat()
        //        });
        //    }
        //    return modellist;
        //}

        public async Task<List<NetWorth>> DownloadNetWorthByDkhs(string fundCode, string dkhsFundCode, int size = 370, int page = 1)
        {
            List<NetWorth> modellist = new List<NetWorth>();
            if (string.IsNullOrWhiteSpace(dkhsFundCode) || string.IsNullOrWhiteSpace(fundCode))
            {
                return modellist;
            }
            string url = $"https://www.dkhs.com/api/v1/symbols/{dkhsFundCode}/net_history/?page={page}&page_size={size}&symbol={dkhsFundCode}";
            var str = await WebSrcUtil.GetToString(url, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(str))
            {
                return modellist;
            }
            var data = JsonConvert.DeserializeObject<DkhsResponse<DkhsNetWorthHistoryResult>>(str);
            if (data?.results == null || data.results.Length <= 0)
            {
                return modellist;
            }
            foreach (var item in data.results)
            {
                var date = item.tradedate.TryToDateTimeOrNull();
                var unitNetWorth = item.net_value;
                var accumulatedNetWorth = item.net_cumulative;
                if (date == null || unitNetWorth == -1 || accumulatedNetWorth == -1)
                {
                    continue;
                }
                modellist.Add(new NetWorth
                {
                    FundCode = fundCode,

                    Date = date.Value,
                    UnitNetWorth = unitNetWorth,
                    AccumulatedNetWorth = accumulatedNetWorth,
                    DailyGrowthRate = item.percent.Replace("%", "").TryToFloat()
                });
            }
            return modellist;
        }
    }
}
