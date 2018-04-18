using Abp.AutoMapper;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(TradeLog))]
    public class TradeLogTransferInput : TradeLogDto
    {
        /// <summary>
        /// 交易份额
        /// </summary>
        public float Portion { get; set; }

        /// <summary>
        /// 转出费率
        /// </summary>
        public float FromServiceRate { get; set; }

        /// <summary>
        /// 转入费率
        /// </summary>
        public float ToServiceRate { get; set; }
    }
}