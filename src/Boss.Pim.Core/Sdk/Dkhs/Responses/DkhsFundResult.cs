namespace Boss.Pim.Sdk.Dkhs.Responses
{
    public class DkhsFundResult
    {
        #region 基本信息

        //public int id { get; set; }
        /// <summary>
        /// 标识编码
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// 基金编码
        /// </summary>
        public string code { get; set; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public int symbol_type { get; set; }
        ///// <summary>
        ///// 子类型id
        ///// </summary>
        //public int symbol_stype { get; set; }
        /// <summary>
        /// 子类型名称
        /// </summary>
        public string symbol_stype_display { get; set; }

        /// <summary>
        /// 基金简称
        /// </summary>
        public string abbr_name { get; set; }

        /// <summary>
        /// 基金简称 首字母
        /// </summary>
        public string chi_spell { get; set; }

        /// <summary>
        /// 基金全称
        /// </summary>
        public string fund_name { get; set; }

        #endregion 基本信息

        ///// <summary>
        ///// 列表状态
        ///// </summary>
        //public int list_status { get; set; }
        //public bool is_margin { get; set; }

        #region 最近交易日信息

        /// <summary>
        /// 单位净值
        /// </summary>
        public float? net_value { get; set; }

        /// <summary>
        /// 累计净值
        /// </summary>
        public float? net_cumulative { get; set; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public string tradedate { get; set; }

        //public float year_yld { get; set; }
        //public float tenthou_unit_incm { get; set; }
        /// <summary>
        /// 上个交易日 收益率
        /// </summary>
        public float? percent_day { get; set; }

        ///// <summary>
        ///// 近 1月 收益率
        ///// </summary>
        //public float? percent_month { get; set; }

        ///// <summary>
        ///// 近 1季度 收益率
        ///// </summary>
        //public float? percent_season { get; set; }

        ///// <summary>
        ///// 近 1周 收益率
        ///// </summary>
        //public float? percent_week { get; set; }

        ///// <summary>
        ///// 近 6个月 收益率
        ///// </summary>
        //public float? percent_six_month { get; set; }

        ///// <summary>
        ///// 近 1年 收益率
        ///// </summary>
        //public float? percent_year { get; set; }

        ///// <summary>
        ///// 近 2年 收益率
        ///// </summary>
        //public float? percent_tyear { get; set; }

        //public float latest_cp_rate { get; set; }

        #endregion 最近交易日信息

        ///// <summary>
        ///// 份额规模
        ///// </summary>
        //public string end_shares { get; set; }

        //public bool is_recommended { get; set; }
        //public string recommend_title { get; set; }
        //public string recommend_desc { get; set; }
        //public bool allow_fixed { get; set; }
        //public string amount_fixed_min { get; set; }
        //public string amount_fixed_max { get; set; }
        //public bool allow_trade { get; set; }
        //public bool allow_buy { get; set; }
        //public bool allow_sell { get; set; }

        #region 基金公司信息

        ///// <summary>
        ///// 基金公司名称
        ///// </summary>
        //public string mana_name { get; set; }

        ///// <summary>
        ///// 基金公司排名
        ///// </summary>
        //public int rank_fund_manager_index { get; set; }

        ///// <summary>
        ///// 总基金公司数
        ///// </summary>
        //public int rank_fund_manager_total { get; set; }

        ///// <summary>
        ///// 公司资产规模
        ///// </summary>
        //public string end_asset { get; set; }

        /// <summary>
        /// 基金公司评分
        /// </summary>
        public int? company_score { get; set; }

        #endregion 基金公司信息

        /// <summary>
        /// 评分
        /// </summary>
        public int? score { get; set; }

        /// <summary>
        /// 分析描述
        /// </summary>
        public string analyse_description { get; set; }

        ///// <summary>
        ///// 资产排名
        ///// </summary>
        //public int? rank_asset_index { get; set; }

        ///// <summary>
        ///// 总资产数
        ///// </summary>
        //public int? rank_asset_total { get; set; }

        /// <summary>
        /// 资产评分
        /// </summary>
        public int? asset_score { get; set; }

        /// <summary>
        /// 盈利能力
        /// </summary>
        public int? profitability { get; set; }

        /// <summary>
        /// 夏普比率
        /// </summary>
        public int? sharpe_ratio { get; set; }

        /// <summary>
        /// 抗风险能力
        /// </summary>
        public int? anti_risk { get; set; }

        /// <summary>
        /// 评分描述
        /// </summary>
        public string score_description { get; set; }

        /// <summary>
        /// 稳定性
        /// </summary>
        public int? stability { get; set; }

        /// <summary>
        /// 超额收益
        /// </summary>
        public int? excess_income { get; set; }

        /// <summary>
        /// 择时能力
        /// </summary>
        public int? timing_selection { get; set; }

        /// <summary>
        /// 股票选择能力
        /// </summary>
        public int? stock_selection { get; set; }

        /// <summary>
        /// 短期评分
        /// </summary>
        public int? score_short { get; set; }

        /// <summary>
        /// 中期评分
        /// </summary>
        public int? score_medium { get; set; }

        /// <summary>
        /// 长期评分
        /// </summary>
        public int? score_long { get; set; }

        public int? index_following { get; set; }
        public int? experience { get; set; }

        //public int trade_status { get; set; }
        //public string fare_ratio_buy { get; set; }
        //public string discount_rate_buy { get; set; }
        //public string fare_ratio_sell { get; set; }
        //public string discount_rate_sell { get; set; }
        //public string amount_min_buy { get; set; }
        //public string amount_max_buy { get; set; }
        //public string shares_min_sell { get; set; }
        //public string shares_max_sell { get; set; }
        //public string shares_min { get; set; }
        //public int investment_risk { get; set; }
        //public string investment_risk_display { get; set; }
        //public int charge_mode { get; set; }
        //public string charge_mode_display { get; set; }
        //public int t_days_sell { get; set; }
        //public int company { get; set; }
        //public float? recommend_percent_value { get; set; }
        //public string recommend_percent_display { get; set; }
        //public bool is_bao { get; set; }
        //public object year_yld_avg_month { get; set; }
        //public object tenthou_unit_incm_avg_month { get; set; }
        //public float? percent_twyear { get; set; }
        //public float? percent_three_year { get; set; }
        //public object percent_five_year { get; set; }
        //public float percent_all { get; set; }

        //public float? recommend_percent_fixed_value { get; set; }
        //public string recommend_percent_fixed_display { get; set; }
        //public int subscription_status { get; set; }
        //public bool allow_subscribe { get; set; }
        //public DateTime? subscribe_start_date { get; set; }
        //public DateTime? subscribe_end_date { get; set; }
        //public string amount_subscribe_min { get; set; }
        //public string amount_subscribe_max { get; set; }
        //public string fare_ratio_subscribe { get; set; }
        //public string discount_rate_subscribe { get; set; }
        //public object track_target { get; set; }
        //public string track_target_name { get; set; }
        ///// <summary>
        ///// 近1年 收益率
        ///// </summary>
        //public float? percent_year_fixed { get; set; }
        ///// <summary>
        ///// 近2年 收益率
        ///// </summary>
        //public float? percent_twyear_fixed { get; set; }
        ///// <summary>
        ///// 近3年 收益率
        ///// </summary>
        //public float? percent_three_year_fixed { get; set; }
        ///// <summary>
        ///// 近5年 收益率
        ///// </summary>
        //public float? percent_five_year_fixed { get; set; }
        //public bool allow_sell2bao { get; set; }
        //public string trade_status_display { get; set; }
        //public string bull_market { get; set; }
        //public string bear_market { get; set; }
        //public string seesawing_market { get; set; }
        //public float? org_heavy_scale { get; set; }
        //public string estab_date { get; set; }
        //public string company_csname { get; set; }
        //public string discount_fare_ratio_buy { get; set; }
        //public string fare_ratio_all { get; set; }
        //public object fnd_stype { get; set; }
        //public object fnd_stype_display { get; set; }
        //public string asset_type_display { get; set; }
        //public string style_type_display { get; set; }
        //public int recommend_period { get; set; }
        //public string recommend_period_display { get; set; }
        //public string recommend_period_length { get; set; }
        //public int recommend_score { get; set; }
        //public DkhsCondition_Tags[] condition_tags { get; set; }
    }
}