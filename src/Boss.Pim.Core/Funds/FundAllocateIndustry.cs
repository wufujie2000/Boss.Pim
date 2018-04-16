using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 行业投资合计
    /// </summary>
    public class FundAllocateIndustry : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 行业名称
        /// </summary>
        [MaxLength(128)]
        public string SectorName { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public float Percent { get; set; }

        /// <summary>
        /// 变动类型 1涨 -1跌
        /// </summary>
        public int ChangeType { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 变动幅度
        /// </summary>
        public float Change { get; set; }
    }
}