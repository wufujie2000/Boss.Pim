using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Funds.ObjectValues;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金交易相关费率
    /// </summary>
    public class TradeRate : AuditedAggregateRoot
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 费率类型
        /// </summary>
        public TradeRateType RateType { get; set; }

        /// <summary>
        /// 金额范围（-1没限制）
        /// </summary>
        public int MoneyRange { get; set; }

        /// <summary>
        /// 金额范围（-1没限制）
        /// </summary>
        public int MinMoneyRange { get; set; }

        /// <summary>
        /// 金额范围（-1没限制）
        /// </summary>
        public int MaxMoneyRange { get; set; }

        /// <summary>
        /// 天数范围（-1没限制）
        /// </summary>
        public int DayRange { get; set; }

        /// <summary>
        /// 天数范围（-1没限制）
        /// </summary>
        public int MinDayRange { get; set; }

        /// <summary>
        /// 天数范围（-1没限制）
        /// </summary>
        public int MaxDayRange { get; set; }

        /// <summary>
        /// 原费率
        /// </summary>
        public float SourceRate { get; set; }

        /// <summary>
        /// 实际费率
        /// </summary>
        public float Rate { get; set; }
    }
}