using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 重仓股票
    /// </summary>
    public class FundAllocateHoldSymbol : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        [MaxLength(Fund.CodeLength)]
        public string Symbol { get; set; }

        /// <summary>
        /// 股票简称
        /// </summary>
        [MaxLength(Fund.ShortNameLength)]
        public string AbbrName { get; set; }

        /// <summary>
        /// 投入金额
        /// </summary>
        public float Asset { get; set; }

        /// <summary>
        /// 投入占比
        /// </summary>
        public float Percent { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 股票类型
        /// </summary>
        public int SymbolType { get; set; }

        /// <summary>
        /// 持仓变动幅度
        /// </summary>
        public float Change { get; set; }

        /// <summary>
        /// 当日涨跌幅
        /// </summary>
        public float Percentage { get; set; }
    }
}