using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金  阶段分析
    /// </summary>
    public class NetWorthPeriodAnalyse : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 阶段起点日期
        /// </summary>
        public DateTime PeriodStartDate { get; set; }

        /// <summary>
        /// 阶段天数
        /// </summary>
        public int PeriodDays { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        public float Avg { get; set; }

        /// <summary>
        /// 月末值
        /// </summary>
        public float Later { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public float Max { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public float Min { get; set; }

        /// <summary>
        /// 平均最大值
        /// </summary>
        public float MaxAvg { get; set; }

        /// <summary>
        /// 平均最小值
        /// </summary>
        public float MinAvg { get; set; }

        /// <summary>
        /// 跌幅值
        /// </summary>
        public float DieFu { get; set; }

        /// <summary>
        /// 涨幅值
        /// </summary>
        public float ZhangFu { get; set; }

        /// <summary>
        /// 波动值
        /// </summary>
        public float BoWave { get; set; }

        /// <summary>
        /// 安全期最低值
        /// </summary>
        public float SafeLow { get; set; }

        /// <summary>
        /// 安全期最高值
        /// </summary>
        public float SafeHigh { get; set; }

        /// <summary>
        /// 安全期买卖价
        /// </summary>
        public float SafeTradeCent { get; set; }

        /// <summary>
        /// 盈利波动
        /// </summary>
        public float PayWaveRate { get; set; }

        /// <summary>
        /// 最大盈利值
        /// </summary>
        public float MaxPayCent { get; set; }

        /// <summary>
        /// 最大亏损净值
        /// </summary>
        public float MaxLoseCent { get; set; }

        /// <summary>
        /// 最低买入值
        /// </summary>
        public float GreatBuy { get; set; }

        /// <summary>
        /// 最高卖出值
        /// </summary>
        public float GreatSale { get; set; }
    }
}