using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Boss.Pim.Funds.Dto;

namespace Boss.Pim.Funds
{
    [AbpAuthorize]
    public class TradeRecordAppService : AsyncCrudAppService<TradeRecord, TradeRecordDto, Guid, PagedAndSortedResultRequestDto, TradeRecordCreateInput, TradeRecordUpdateInput>, ITradeRecordAppService
    {
        public IRepository<TradeRecord, Guid> TradeRecordRepository { get; set; }
        public IRepository<NetWorth, Guid> NetWorthRepository { get; set; }
        public IRepository<Fund> FundRepository { get; set; }

        public TradeRecordAppService(IRepository<TradeRecord, Guid> repository) : base(repository)
        {
        }

        public override async Task<TradeRecordDto> Create(TradeRecordCreateInput input)
        {
            var info = new TradeRecord
            {
                UserId = AbpSession.UserId ?? 0,
                FundCode = input.FundCode,
                BuyTime = input.BuyTime,
                BuyAmount = input.BuyAmount
            };
            await UpdateNetWorth(info);
            await Repository.InsertAsync(info);
            return info.MapTo<TradeRecordDto>();
        }

        public override async Task<TradeRecordDto> Update(TradeRecordUpdateInput input)
        {
            var info = await Repository.GetAsync(input.Id);

            await UpdateNetWorth(info);

            return info.MapTo<TradeRecordDto>();
        }

        public async Task UpdateAll()
        {
            var userid = AbpSession.UserId;
            var list = TradeRecordRepository.GetAll().Where(a => a.UserId == userid).ToList();
            foreach (var info in list)
            {
                await UpdateNetWorth(info);
            }
        }

        private async Task UpdateNetWorth(TradeRecord info)
        {
            var buyDate = info.BuyTime.Date;
            var buyDayModel = await AsyncQueryableExecuter.FirstOrDefaultAsync(
                NetWorthRepository.GetAll().Where(a => a.Date == buyDate).Select(b => b.UnitNetWorth)
                );

            info.BuyUnitNetWorth = buyDayModel;
        }
    }
}