using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 阶段涨幅
    /// </summary>
    public class PeriodIncrease : AuditedAggregateRoot
    {
        public const int TitleMaxLength = 64;

        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        /// <summary>
        /// 收益率
        /// </summary>
        public float ReturnRate { get; set; }

        /// <summary>
        /// 同类平均
        /// </summary>
        public float SameTypeAverage { get; set; }

        /// <summary>
        /// 沪深300
        /// </summary>
        public float Hs300 { get; set; }

        /// <summary>
        /// 同类排名
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 同类总数
        /// </summary>
        public int SameTypeTotalQty { get; set; }

        /// <summary>
        /// 不同数量
        /// </summary>
        public int DifferentQty { get; set; }

        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime ClosingDate { get; set; }
    }
}