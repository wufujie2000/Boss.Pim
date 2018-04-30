using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Funds.Values;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金交易日志
    /// </summary>
    public class TradeLog : AuditedAggregateRoot<Guid>
    {
        public long UserId { get; set; }

        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 交易时的单位净值
        /// </summary>
        public float UnitNetWorth { get; set; }

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
        /// 交易手续费
        /// </summary>
        public float ServiceCharge { get; set; }

        /// <summary>
        /// 交易记录类型
        /// </summary>
        public TradeRecordType TradeType { get; set; }
    }
}