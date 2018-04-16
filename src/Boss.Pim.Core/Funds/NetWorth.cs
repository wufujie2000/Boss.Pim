using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Attributes;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金净值
    /// </summary>
    public class NetWorth : AuditedAggregateRoot<Guid>
    {
        public const int PurchaseStatusMaxLength = 128;
        public const int RedemptionStateMaxLength = 128;
        public const int DividendsDistributionMaxLength = 128;

        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 单位净值
        /// </summary>
        public float UnitNetWorth { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>
        public float AccumulatedNetWorth { get; set; }

        /// <summary>
        /// 日增长率
        /// </summary>        
        public float DailyGrowthRate { get; set; }

        /// <summary>
        /// 申购状态
        /// </summary>
        [MaxLength(PurchaseStatusMaxLength)]
        public string PurchaseStatus { get; set; }

        /// <summary>
        /// 赎回状态
        /// </summary>
        [MaxLength(RedemptionStateMaxLength)]
        public string RedemptionState { get; set; }

        /// <summary>
        /// 分红配送
        /// </summary>
        [MaxLength(DividendsDistributionMaxLength)]
        public string DividendsDistribution { get; set; }
    }
}