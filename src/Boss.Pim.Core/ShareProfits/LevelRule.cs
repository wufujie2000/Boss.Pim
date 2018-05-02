using Abp.Domain.Entities.Auditing;
using Boss.Pim.ShareProfits.Values;

namespace Boss.Pim.ShareProfits
{
    public class LevelRule : FullAuditedAggregateRoot
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 级别类型
        /// </summary>
        public LevelType LevelType { get; set; }

        /// <summary>
        /// 补仓比例
        /// </summary>
        public decimal Proportion { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int ProductQty { get; set; }

        /// <summary>
        /// 购买等级所需金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}