using Abp.AutoMapper;

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
    }
}