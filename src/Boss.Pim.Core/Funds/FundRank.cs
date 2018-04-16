using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金排名
    /// </summary>
    public class FundRank : AuditedAggregateRoot<Guid>
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
        /// 周增长率
        /// </summary>
        public float ZGrowthRate { get; set; }

        /// <summary>
        /// 月增长率
        /// </summary>
        public float YGrowthRate { get; set; }

        /// <summary>
        /// 3月增长率
        /// </summary>
        public float Y3GrowthRate { get; set; }

        /// <summary>
        /// 6月增长率
        /// </summary>
        public float Y6GrowthRate { get; set; }

        /// <summary>
        /// 1年增长率
        /// </summary>
        public float N1GrowthRate { get; set; }

        /// <summary>
        /// 2年增长率
        /// </summary>
        public float N2GrowthRate { get; set; }

        /// <summary>
        /// 3年增长率
        /// </summary>
        public float N3GrowthRate { get; set; }

        /// <summary>
        /// 5年增长率
        /// </summary>
        public float N5GrowthRate { get; set; }

        /// <summary>
        /// 今年增长率
        /// </summary>
        public float JNGrowthRate { get; set; }

        /// <summary>
        /// 历年增长率
        /// </summary>
        public float LNGrowthRate { get; set; }
    }
}