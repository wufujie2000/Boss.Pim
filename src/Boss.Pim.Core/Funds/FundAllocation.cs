using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    public class FundAllocation : AuditedAggregateRoot
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 股票金额
        /// </summary>
        public float SymbolAsset { get; set; }

        /// <summary>
        /// 股票占比
        /// </summary>
        public float SymbolPercent { get; set; }

        /// <summary>
        /// 债券占比
        /// </summary>
        public float BondAsset { get; set; }

        /// <summary>
        /// 债券占比
        /// </summary>
        public float BondPercent { get; set; }

        /// <summary>
        /// 现金金额
        /// </summary>
        public float CashAsset { get; set; }

        /// <summary>
        /// 现金占比
        /// </summary>
        public float CashPercent { get; set; }

        /// <summary>
        /// 其它金额
        /// </summary>
        public float OtherAsset { get; set; }

        /// <summary>
        /// 其它占比
        /// </summary>
        public float OtherPercent { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [MaxLength(64)]
        public string EndDate { get; set; }
    }
}