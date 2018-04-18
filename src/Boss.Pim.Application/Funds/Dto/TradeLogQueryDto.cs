using Abp.AutoMapper;
using Boss.Pim.Funds.ObjectValues;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(TradeLog))]
    public class TradeLogQueryDto : TradeLogDto
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// 交易份额
        /// </summary>
        public float Portion { get; set; }

        /// <summary>
        /// 交易费率
        /// </summary>
        public float ServiceRate { get; set; }

        /// <summary>
        /// 交易时的单位净值
        /// </summary>
        public float UnitNetWorth { get; set; }

        /// <summary>
        /// 交易手续费
        /// </summary>
        public float ServiceCharge { get; set; }

        /// <summary>
        /// 交易记录类型
        /// </summary>
        public TradeRecordType TradeType { get; set; }
    }
}