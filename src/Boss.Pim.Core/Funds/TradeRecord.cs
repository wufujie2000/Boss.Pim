using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Funds.Values;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金交易记录
    /// </summary>
    public class TradeRecord : AuditedAggregateRoot<Guid>
    {
        public long UserId { get; set; }

        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }

        /// <summary>
        /// 购买时的单位净值
        /// </summary>
        public float BuyUnitNetWorth { get; set; }

        /// <summary>
        /// 购买金额
        /// </summary>
        public float BuyAmount { get; set; }

        /// <summary>
        /// 确认金额
        /// </summary>
        public float ConfirmAmount { get; set; }

        /// <summary>
        /// 确认份额
        /// </summary>
        public float ConfirmShare { get; set; }

        /// <summary>
        /// 购买手续费
        /// </summary>
        public float BuyServiceCharge { get; set; }

        /// <summary>
        /// 交易记录类型
        /// </summary>
        public TradeRecordType TradeRecordType { get; set; }
    }
}