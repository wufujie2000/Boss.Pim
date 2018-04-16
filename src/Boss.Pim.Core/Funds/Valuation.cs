using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Attributes;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金估值
    /// </summary>
    public class Valuation : AuditedAggregateRoot
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 估值来源平台
        /// </summary>
        [MaxLength(256)]
        public string SourcePlatform { get; set; }

        /// <summary>
        /// 估值时间
        /// </summary>
        public DateTime EstimatedTime { get; set; }

        /// <summary>
        /// 上次单位净值
        /// </summary>
        public float LastUnitNetWorth { get; set; }

        /// <summary>
        /// 估值单位净值
        /// </summary>
        public float EstimatedUnitNetWorth { get; set; }

        /// <summary>
        /// 估值 收益率
        /// </summary>
        public float ReturnRate { get; set; }
    }
}