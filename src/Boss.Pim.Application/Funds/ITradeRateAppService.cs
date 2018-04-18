using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface ITradeRateAppService : IAsyncCrudAppService<TradeRateDto>
    {
        Task DownloadTrade();
        Task Download();
        Task DownloadOptional();
        Task DownloadByFundCode(List<string> fundCodes);
        Task DownloadByFuncode(string fund);
    }
}