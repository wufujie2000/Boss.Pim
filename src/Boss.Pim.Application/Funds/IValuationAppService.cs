using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface IValuationAppService : IAsyncCrudAppService<ValuationDto>
    {
        Task DownloadAllFundEasyMoney();

        Task DownloadOptionalEasyMoney();

        Task DownoadByFundCode(string fundCodes);
    }
}