using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Boss.Pim.Funds.Dto;
using Boss.Pim.Funds.Values;

namespace Boss.Pim.Funds
{
    [AbpAuthorize]
    public class TradeLogAppService : AsyncCrudAppService<TradeLog, TradeLogQueryDto, Guid, PagedAndSortedResultRequestDto>, ITradeLogAppService
    {
        public IRepository<NetWorth, Guid> NetWorthRepository { get; set; }
        public IRepository<Fund> FundRepository { get; set; }
        public object input { get; private set; }

        public TradeLogAppService(IRepository<TradeLog, Guid> repository) : base(repository)
        {
        }

        #region Buy
        public async Task Buy(TradeLogBuyInput input)
        {
            await Buy(input.FundCode, input.Time, input.Amount, input.ServiceRate, input.TradeType);
        }

        private async Task Buy(string fundCode, DateTime time, float amount, float serviceRate, TradeRecordType recordType)
        {
            var info = new TradeLog
            {
                UserId = AbpSession.UserId ?? 0,
                FundCode = fundCode,
                Time = time,
                Amount = amount / (1 + serviceRate / 100),
                ServiceRate = serviceRate,
                TradeType = recordType
            };
            info.ServiceCharge = info.Amount * serviceRate / 100;

            //内扣法是针对认购金额，即投资总额的。
            //认购费用＝ 认购金额×认购费率
            //净认购金额＝认购金额－认购费用
            //认购份额＝ 净认购金额 / 基金单位面值

            //外扣法是针对实际认购金额，即净投资额的。
            //净认购金额＝认购金额 / (1＋认购费率) 
            //认购费用＝净认购金额×认购费率
            //认购份额＝(认购金额－认购费用)/ 基金份额面值

            await UpdateBuyUniteNetWorth(info);
            await Repository.InsertAsync(info);
        }

        public async Task UpdateAllBuyUniteNetWorth()
        {
            var userid = AbpSession.UserId;
            var list = Repository.GetAll().Where(a => a.UserId == userid && (a.TradeType == TradeRecordType.买入 || a.TradeType == TradeRecordType.转入)).ToList();
            foreach (var info in list)
            {
                await UpdateBuyUniteNetWorth(info);
            }
        }

        private async Task UpdateBuyUniteNetWorth(TradeLog info)
        {
            info.UnitNetWorth = await GetUnitNetWorth(info);
            if (info.UnitNetWorth > 0)
            {
                info.Portion = info.Amount / info.UnitNetWorth;
            }
        }
        #endregion

        #region Sell
        public async Task Sell(TradeLogSellInput input)
        {
            await Sell(input.FundCode, input.Time, input.Portion, input.ServiceRate, input.TradeType, input.Amount);
        }

        private async Task<TradeLog> Sell(string fundCode, DateTime time, float portion, float serviceRate, TradeRecordType recordType, float amount)
        {
            var info = new TradeLog
            {
                UserId = AbpSession.UserId ?? 0,
                FundCode = fundCode,
                Time = time,
                Portion = portion,
                Amount = amount,
                ServiceRate = serviceRate,
                TradeType = recordType
            };
            await UpdateSellUniteNetWorth(info);
            await Repository.InsertAsync(info);
            return info;
        }

        public async Task UpdateAllSellUniteNetWorth()
        {
            var userid = AbpSession.UserId;
            var list = Repository.GetAll().Where(a => a.UserId == userid && (a.TradeType == TradeRecordType.卖出 || a.TradeType == TradeRecordType.转出)).ToList();
            foreach (var info in list)
            {
                await UpdateBuyUniteNetWorth(info);
            }
        }

        private async Task UpdateSellUniteNetWorth(TradeLog info)
        {
            info.UnitNetWorth = await GetUnitNetWorth(info);
            if (info.UnitNetWorth > 0)
            {
                if (info.Amount > 0)
                {
                    info.Portion = info.Amount / info.UnitNetWorth;
                }
                else
                {
                    info.Amount = info.Portion * info.UnitNetWorth;
                }
                info.ServiceCharge = info.Amount * info.ServiceRate / 100;
            }
        }
        #endregion        

        private async Task<float> GetUnitNetWorth(TradeLog info)
        {
            var date = info.Time.Date;
            var unitNetWorth = await AsyncQueryableExecuter.FirstOrDefaultAsync(
                NetWorthRepository.GetAll()
                .Where(a => a.Date == date && a.FundCode == info.FundCode)
                .Select(b => b.UnitNetWorth)
                );
            return unitNetWorth;
        }
    }
}