using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Sdk.Dkhs.Responses
{
    public class DkhsAssetAllocation
    {
        public int id { get; set; }
        public int fund { get; set; }
        /// <summary>
        /// 基金名称
        /// </summary>
        public string fund_sname { get; set; }
        /// <summary>
        /// 股票金额
        /// </summary>
        public string symbol_asset { get; set; }
        /// <summary>
        /// 股票占比
        /// </summary>
        public string symbol_percent { get; set; }
        /// <summary>
        /// 债券占比
        /// </summary>
        public string bond_asset { get; set; }
        /// <summary>
        /// 债券占比
        /// </summary>
        public string bond_percent { get; set; }
        /// <summary>
        /// 现金金额
        /// </summary>
        public string cash_asset { get; set; }
        /// <summary>
        /// 现金占比
        /// </summary>
        public string cash_percent { get; set; }
        /// <summary>
        /// 其它金额
        /// </summary>
        public string other_asset { get; set; }
        /// <summary>
        /// 其它占比
        /// </summary>
        public string other_percent { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public string end_date { get; set; }
        public DkhsIndustry[] industry { get; set; }
        public DkhsHold_Symbol[] hold_symbol { get; set; }
    }

    /// <summary>
    /// 行业投资合计
    /// </summary>
    public class DkhsIndustry
    {
        /// <summary>
        /// 行业id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 行业名称
        /// </summary>
        public string sector_name { get; set; }
        /// <summary>
        /// 占比
        /// </summary>
        public string percent { get; set; }
        /// <summary>
        /// 变动类型 1涨 -1跌
        /// </summary>
        public int change_type { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// 变动幅度
        /// </summary>
        public float change { get; set; }
    }

    /// <summary>
    /// 重仓股票
    /// </summary>
    public class DkhsHold_Symbol
    {
        public int id { get; set; }
        /// <summary>
        /// 股票代码
        /// </summary>
        public string symbol { get; set; }
        /// <summary>
        /// 股票简称
        /// </summary>
        public string abbr_name { get; set; }
        /// <summary>
        /// 投入金额
        /// </summary>
        public string asset { get; set; }
        /// <summary>
        /// 投入占比
        /// </summary>
        public string percent { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public string end_date { get; set; }
        /// <summary>
        /// 股票类型
        /// </summary>
        public int symbol_type { get; set; }
        /// <summary>
        /// 持仓变动幅度
        /// </summary>
        public float change { get; set; }
        /// <summary>
        /// 当日涨跌幅
        /// </summary>
        public string percentage { get; set; }
    }

}
