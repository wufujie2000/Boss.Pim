using Abp.AutoMapper;
using Boss.Pim.Funds.Values;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(TradeLog))]
    public class TradeLogSellInput : TradeLogDto
    {
        /// <summary>
        /// 交易份额
        /// </summary>
        public float Portion { get; set; }

        /// <summary>
        /// 交易费率
        /// </summary>
        public float ServiceRate { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TradeRecordType TradeType { get; set; }
    }
}