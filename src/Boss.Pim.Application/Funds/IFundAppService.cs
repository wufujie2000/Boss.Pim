using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface IFundAppService : IAsyncCrudAppService<FundDto>
    {
        Task Download();
        Task SetOptional(List<string> fundCodes);
        Task SetNotTradeFunds(List<string> fundCodes);
        Task SetNotTradeFund(string fundCode);

        //void EastmoneyGet(string code);
    }
}