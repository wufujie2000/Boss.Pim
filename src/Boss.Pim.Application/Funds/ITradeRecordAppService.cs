using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    public interface ITradeRecordAppService : IAsyncCrudAppService<TradeRecordDto, Guid, PagedAndSortedResultRequestDto, TradeRecordCreateInput, TradeRecordUpdateInput>
    {
        /// <summary>
        /// 更新所有交易
        /// </summary>
        /// <returns></returns>
        Task UpdateAll();
    }
}