using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface ITradeLogAppService : IAsyncCrudAppService<TradeLogQueryDto, Guid, PagedAndSortedResultRequestDto>
    {
        Task Buy(TradeLogBuyInput input);

        Task UpdateAllBuyUniteNetWorth();

        Task Sell(TradeLogSellInput input);

        Task UpdateAllSellUniteNetWorth();
    }
}