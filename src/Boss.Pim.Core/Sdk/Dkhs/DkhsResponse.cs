using System;
using Boss.Pim.Sdk.Dkhs.Responses;

namespace Boss.Pim.Sdk.Dkhs
{
    public class DkhsResponse<TResult>
    {
        public int total_count { get; set; }
        public int total_page { get; set; }
        public int current_page { get; set; }
        public TResult[] results { get; set; }
    }



    public class Rootobject
    {
        public DkhsFund_Managers[] fund_managers { get; set; }
        public string track_target_sname { get; set; }
        public DkhsRecommend_Funds[] recommend_funds { get; set; }
        public string fund_similar_stat_description { get; set; }
        public string analyse_date { get; set; }
        public DkhsFund fund { get; set; }
        public DkhsCompany company { get; set; }
        public string fund_asset_description { get; set; }
        public string fund_company_description { get; set; }
        public object id { get; set; }
        public DkhsFund_Similar_Stat[] fund_similar_stat { get; set; }
        public bool allow_score { get; set; }
        public string fund_manager_description { get; set; }
        public DkhsFund_Profit_Loss_Predicts[] fund_profit_loss_predicts { get; set; }
        public object[] track_target_pe { get; set; }
        public bool is_recommended { get; set; }
        public bool is_alert { get; set; }
        public string track_target_pe_desc { get; set; }
        public DkhsPeriod_Analyse_Stat2[] period_analyse_stat { get; set; }
    }

    public class DkhsFund
    {
        public float percent_tyear { get; set; }
        public string code { get; set; }
        public bool followed { get; set; }
        public float percent_twyear_fixed { get; set; }
        public string score_description { get; set; }
        public string shares_max_sell { get; set; }
        public float year_yld { get; set; }
        public int investment_risk { get; set; }
        public int list_status { get; set; }
        public int anti_risk { get; set; }
        public int stability { get; set; }
        public int score_long { get; set; }
        public string amount_min_buy { get; set; }
        public string style_type_display { get; set; }
        public float percent_week { get; set; }
        public string end_asset { get; set; }
        public int symbol_type { get; set; }
        public int sharpe_ratio { get; set; }
        public string discount_rate_buy { get; set; }
        public bool allow_buy { get; set; }
        public int excess_income { get; set; }
        public float tenthou_unit_incm { get; set; }
        public float percent_year_fixed { get; set; }
        public float percent_three_year_fixed { get; set; }
        public string shares_min { get; set; }
        public string chi_spell { get; set; }
        public object year_yld_avg_month { get; set; }
        public string shares_min_sell { get; set; }
        public object track_target { get; set; }
        public string discount_rate_sell { get; set; }
        public int timing_selection { get; set; }
        public string recommend_percent_fixed_display { get; set; }
        public int rank_asset_total { get; set; }
        public int company { get; set; }
        public int subscription_status { get; set; }
        public bool is_margin { get; set; }
        public int index_following { get; set; }
        public string bull_market { get; set; }
        public float org_heavy_scale { get; set; }
        public float percent_three_year { get; set; }
        public float percent_twyear { get; set; }
        public int trade_status { get; set; }
        public bool allow_sell { get; set; }
        public bool allow_fixed { get; set; }
        public string trade_status_display { get; set; }
        public string estab_date { get; set; }
        public int rank_asset_index { get; set; }
        public string tradedate { get; set; }
        public string score_display { get; set; }
        public int charge_mode { get; set; }
        public string discount_fare_ratio_buy { get; set; }
        public int rank_fund_manager_index { get; set; }
        public string symbol_stype_display { get; set; }
        public int stock_selection { get; set; }
        public string fare_ratio_all { get; set; }
        public string fare_ratio_buy { get; set; }
        public bool allow_sell2bao { get; set; }
        public string analyse_description { get; set; }
        public float percent_six_month { get; set; }
        public float percent_month { get; set; }
        public string recommend_percent_display { get; set; }
        public string recommend_period_length { get; set; }
        public int score_medium { get; set; }
        public float net_value { get; set; }
        public object fnd_stype { get; set; }
        public string recommend_period_display { get; set; }
        public object fnd_stype_display { get; set; }
        public string fare_ratio_subscribe { get; set; }
        public int symbol_stype { get; set; }
        public int score_short { get; set; }
        public bool is_bao { get; set; }
        public string bear_market { get; set; }
        public string mana_name { get; set; }
        public string company_csname { get; set; }
        public object track_target_name { get; set; }
        public int profitability { get; set; }
        public int recommend_score { get; set; }
        public int id { get; set; }
        public string amount_subscribe_min { get; set; }
        public float percent_year { get; set; }
        public int score { get; set; }
        public object tenthou_unit_incm_avg_month { get; set; }
        public int t_days_sell { get; set; }
        public object percent_five_year { get; set; }
        public string advise { get; set; }
        public bool allow_trade { get; set; }
        public string abbr_name { get; set; }
        public float recommend_percent_value { get; set; }
        public float latest_cp_rate { get; set; }
        public object recommend_title { get; set; }
        public int star { get; set; }
        public string fund_name { get; set; }
        public string symbol { get; set; }
        public string end_shares { get; set; }
        public string seesawing_market { get; set; }
        public object recommend_desc { get; set; }
        public string amount_fixed_max { get; set; }
        public float percent_day { get; set; }
        public int asset_score { get; set; }
        public string amount_fixed_min { get; set; }
        public int rank_fund_manager_total { get; set; }
        public float percent_season { get; set; }
        public int recommend_period { get; set; }
        public string amount_subscribe_max { get; set; }
        public string fare_ratio_sell { get; set; }
        public float net_cumulative { get; set; }
        public string asset_type_display { get; set; }
        public int company_score { get; set; }
        public string discount_rate_subscribe { get; set; }
        public string charge_mode_display { get; set; }
        public int experience { get; set; }
        public float percent_all { get; set; }
        public float recommend_percent_fixed_value { get; set; }
        public string investment_risk_display { get; set; }
        public bool allow_subscribe { get; set; }
        public float percent_five_year_fixed { get; set; }
        public DateTime subscribe_start_date { get; set; }
        public bool is_recommended { get; set; }
        public DateTime subscribe_end_date { get; set; }
        public string amount_max_buy { get; set; }
    }

    public class DkhsCompany
    {
        public string assets_total { get; set; }
        public int fund_manager_total { get; set; }
        public float win_rate_asset { get; set; }
        public int fund_total { get; set; }
        public string regi_cap { get; set; }
        public object rank_index_fund { get; set; }
        public object rank_total_fund { get; set; }
        public string logo { get; set; }
        public object curncy { get; set; }
        public int id { get; set; }
        public object rank_total_fund_manager { get; set; }
        public object rank_index_fund_manager { get; set; }
        public string build_date { get; set; }
        public object rank_index_asset { get; set; }
        public int rank_total { get; set; }
        public string csname { get; set; }
        public float score_fund_manager { get; set; }
        public string cname { get; set; }
        public object rank_total_asset { get; set; }
        public float win_rate_fund_manager { get; set; }
        public int rank_index { get; set; }
        public string com_brief { get; set; }
        public float win_rate_score_fund_manager { get; set; }
    }

    /// <summary>
    /// 基金经理信息
    /// </summary>
    public class DkhsFund_Managers
    {
        /// <summary>
        /// 5年收益
        /// </summary>
        public float index_rate_five_year { get; set; }
        /// <summary>
        /// 评分描述
        /// </summary>
        public string score_description { get; set; }
        public float win_rate_day { get; set; }
        public string sharpe_ratio_display { get; set; }
        public int anti_risk { get; set; }
        public string avatar_md { get; set; }
        public Manager_Analyse_Collects[] manager_analyse_collects { get; set; }
        public int sharpe_ratio { get; set; }
        public int excess_income { get; set; }
        public string anti_risk_display { get; set; }
        public float win_rate_year { get; set; }
        public int ability_avg { get; set; }
        public int timing_selection { get; set; }
        public string start_date { get; set; }
        public bool allow_analyse { get; set; }
        public object end_date { get; set; }
        public float win_rate_week { get; set; }
        public float index_rate_season { get; set; }
        public string index_rank_month { get; set; }
        public int index_following { get; set; }
        public string avatar_lg { get; set; }
        public int bull_market { get; set; }
        public float index_rate_week { get; set; }
        public int stability { get; set; }
        public string index_rank_tyear { get; set; }
        /// <summary>
        /// 经理姓名
        /// </summary>
        public string name { get; set; }
        public float index_rate_twyear { get; set; }
        public object win_rate_twyear { get; set; }
        public int stock_selection { get; set; }
        public float index_rate_all { get; set; }
        public string avatar_xs { get; set; }
        public string recommend_percent_display { get; set; }
        public float index_rate_day { get; set; }
        public Analyse_History[] analyse_history { get; set; }
        public bool allow_score { get; set; }
        public int id { get; set; }
        public float index_rate_three_year { get; set; }
        public float index_rate_year { get; set; }
        /// <summary>
        /// 基金公司名称
        /// </summary>
        public string fund_company { get; set; }
        public float win_rate_month { get; set; }
        public DkhsRecommend_Fund recommend_fund { get; set; }
        public int bear_market { get; set; }
        public int profitability { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string work_seniority { get; set; }
        public int score { get; set; }
        public float index_rate_month { get; set; }
        public string rate_date { get; set; }
        public float index_rate_tyear { get; set; }
        public float recommend_percent_value { get; set; }
        public float index_rate_six_month { get; set; }
        public int good_at_type { get; set; }
        public string recommend_title { get; set; }
        public Manager_Analyse_Collect manager_analyse_collect { get; set; }
        public Period_Analyse_Stat[] period_analyse_stat { get; set; }
        public string avatar_sm { get; set; }
        public string profitability_display { get; set; }
        public float win_rate_tyear { get; set; }
        public float win_rate_season { get; set; }
        public int seesawing_market { get; set; }
        public string recommend_desc { get; set; }
        public string latest_view { get; set; }
        public int market_avg { get; set; }
        public float win_rate_six_month { get; set; }
        public string index_rank_year { get; set; }
        public int experience { get; set; }
        public int post_status { get; set; }
        public float win_rate_three_year { get; set; }
        public int manager_type { get; set; }
        public bool is_recommended { get; set; }
        public int recommend_percent { get; set; }
    }

    public class DkhsRecommend_Fund
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
        public int symbol_type { get; set; }
        public int symbol_stype { get; set; }
        public string symbol_stype_display { get; set; }
        public string abbr_name { get; set; }
        public string chi_spell { get; set; }
        public int list_status { get; set; }
        public bool is_margin { get; set; }
        public float net_value { get; set; }
        public float net_cumulative { get; set; }
        public string tradedate { get; set; }
        public float year_yld { get; set; }
        public float tenthou_unit_incm { get; set; }
        public float percent_day { get; set; }
        public float percent_month { get; set; }
        public float percent_season { get; set; }
        public float percent_week { get; set; }
        public float percent_six_month { get; set; }
        public float percent_year { get; set; }
        public float percent_tyear { get; set; }
        public float latest_cp_rate { get; set; }
        public string end_shares { get; set; }
        public bool is_recommended { get; set; }
        public object recommend_title { get; set; }
        public object recommend_desc { get; set; }
        public bool allow_fixed { get; set; }
        public string amount_fixed_min { get; set; }
        public string amount_fixed_max { get; set; }
        public bool allow_trade { get; set; }
        public bool allow_buy { get; set; }
        public bool allow_sell { get; set; }
        public int trade_status { get; set; }
        public string fare_ratio_buy { get; set; }
        public string discount_rate_buy { get; set; }
        public string fare_ratio_sell { get; set; }
        public string discount_rate_sell { get; set; }
        public string amount_min_buy { get; set; }
        public string amount_max_buy { get; set; }
        public string shares_min_sell { get; set; }
        public string shares_max_sell { get; set; }
        public string shares_min { get; set; }
        public int investment_risk { get; set; }
        public string investment_risk_display { get; set; }
        public int charge_mode { get; set; }
        public string charge_mode_display { get; set; }
        public int t_days_sell { get; set; }
        public int company { get; set; }
        public float recommend_percent_value { get; set; }
        public string recommend_percent_display { get; set; }
        public bool is_bao { get; set; }
        public object year_yld_avg_month { get; set; }
        public object tenthou_unit_incm_avg_month { get; set; }
        public float percent_twyear { get; set; }
        public float percent_three_year { get; set; }
        public object percent_five_year { get; set; }
        public float percent_all { get; set; }
        public int score { get; set; }
        public string analyse_description { get; set; }
        public string mana_name { get; set; }
        public string fund_name { get; set; }
        public int rank_fund_manager_index { get; set; }
        public int rank_fund_manager_total { get; set; }
        public int rank_asset_index { get; set; }
        public int rank_asset_total { get; set; }
        public string end_asset { get; set; }
        public int company_score { get; set; }
        public int asset_score { get; set; }
        public int profitability { get; set; }
        public int sharpe_ratio { get; set; }
        public int anti_risk { get; set; }
        public float recommend_percent_fixed_value { get; set; }
        public string recommend_percent_fixed_display { get; set; }
        public int subscription_status { get; set; }
        public bool allow_subscribe { get; set; }
        public DateTime subscribe_start_date { get; set; }
        public DateTime subscribe_end_date { get; set; }
        public string amount_subscribe_min { get; set; }
        public string amount_subscribe_max { get; set; }
        public string fare_ratio_subscribe { get; set; }
        public string discount_rate_subscribe { get; set; }
        public object track_target { get; set; }
        public object track_target_name { get; set; }
        public float percent_year_fixed { get; set; }
        public float percent_twyear_fixed { get; set; }
        public float percent_three_year_fixed { get; set; }
        public float percent_five_year_fixed { get; set; }
        public string score_description { get; set; }
        public int stability { get; set; }
        public int excess_income { get; set; }
        public int index_following { get; set; }
        public int experience { get; set; }
        public int timing_selection { get; set; }
        public int stock_selection { get; set; }
        public bool allow_sell2bao { get; set; }
        public string trade_status_display { get; set; }
        public string bull_market { get; set; }
        public string bear_market { get; set; }
        public string seesawing_market { get; set; }
        public float org_heavy_scale { get; set; }
        public string estab_date { get; set; }
        public string company_csname { get; set; }
        public string discount_fare_ratio_buy { get; set; }
        public string fare_ratio_all { get; set; }
        public object fnd_stype { get; set; }
        public object fnd_stype_display { get; set; }
        public string asset_type_display { get; set; }
        public string style_type_display { get; set; }
        public int score_short { get; set; }
        public int score_medium { get; set; }
        public int score_long { get; set; }
        public int recommend_period { get; set; }
        public string recommend_period_display { get; set; }
        public string recommend_period_length { get; set; }
        public int recommend_score { get; set; }
    }

    public class Manager_Analyse_Collect
    {
        public int excess_income { get; set; }
        public int manager_type_display { get; set; }
        public int score { get; set; }
        public int bear_market { get; set; }
        public int period { get; set; }
        public int experience { get; set; }
        public int anti_risk { get; set; }
        public int stability { get; set; }
        public int profitability { get; set; }
        public int stock_selection { get; set; }
        public int index_following { get; set; }
        public int manager_type { get; set; }
        public int period_display { get; set; }
        public int retreat_rate { get; set; }
        public int timing_selection { get; set; }
        public int sharpe_ratio { get; set; }
        public int seesawing_market { get; set; }
        public int id { get; set; }
        public int percent_relative { get; set; }
        public int bull_market { get; set; }
    }

    public class Manager_Analyse_Collects
    {
        public int id { get; set; }
        public int manager_type { get; set; }
        public int period { get; set; }
        public string period_display { get; set; }
        public string manager_type_display { get; set; }
        public string profitability { get; set; }
        public string sharpe_ratio { get; set; }
        public string anti_risk { get; set; }
        public string retreat_rate { get; set; }
        public string percent_relative { get; set; }
        public string timing_selection { get; set; }
        public string stock_selection { get; set; }
        public string index_following { get; set; }
        public string stability { get; set; }
        public string excess_income { get; set; }
        public string management_experience { get; set; }
        public string management_asset { get; set; }
        public string management_fund_total { get; set; }
        public string timing_selection_hs300 { get; set; }
        public string stock_selection_hs300 { get; set; }
        public string fluctuate_rate { get; set; }
    }

    public class Analyse_History
    {
        public int id { get; set; }
        public int period { get; set; }
        public string period_display { get; set; }
        public string profitability { get; set; }
        public string sharpe_ratio { get; set; }
        public string anti_risk { get; set; }
        public string retreat_rate { get; set; }
        public string percent_relative { get; set; }
        public string timing_selection { get; set; }
        public string stock_selection { get; set; }
        public string index_following { get; set; }
        public string stability { get; set; }
        public string excess_income { get; set; }
        public string management_experience { get; set; }
        public string management_asset { get; set; }
        public int management_fund_total { get; set; }
        public string fluctuate_rate { get; set; }
        public bool allow_timing_selection { get; set; }
        public bool allow_stock_selection { get; set; }
        public string timing_selection_begin_date { get; set; }
        public string timing_selection_end_date { get; set; }
        public string stock_selection_begin_date { get; set; }
        public string stock_selection_end_date { get; set; }
        public string index_following_display { get; set; }
        public string excess_income_display { get; set; }
    }

    public class Period_Analyse_Stat
    {
        public bool allow_analyse { get; set; }
        public int excess_income { get; set; }
        public Manager_Analyse_Collect1 manager_analyse_collect { get; set; }
        public int score { get; set; }
        public string period_length { get; set; }
        public string score_description { get; set; }
        public int period { get; set; }
        public int experience { get; set; }
        public int anti_risk { get; set; }
        public int stability { get; set; }
        public int profitability { get; set; }
        public int stock_selection { get; set; }
        public int index_following { get; set; }
        public string period_display { get; set; }
        public int timing_selection { get; set; }
        public int sharpe_ratio { get; set; }
        public bool allow_score { get; set; }
    }

    public class Manager_Analyse_Collect1
    {
        public int excess_income { get; set; }
        public int score { get; set; }
        public int bear_market { get; set; }
        public int profitability { get; set; }
        public int experience { get; set; }
        public int seesawing_market { get; set; }
        public int stability { get; set; }
        public int stock_selection { get; set; }
        public int index_following { get; set; }
        public int bull_market { get; set; }
        public int timing_selection { get; set; }
        public int sharpe_ratio { get; set; }
        public int anti_risk { get; set; }
    }

    public class DkhsRecommend_Funds
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string code { get; set; }
        public int symbol_type { get; set; }
        public int symbol_stype { get; set; }
        public string symbol_stype_display { get; set; }
        public string abbr_name { get; set; }
        public string chi_spell { get; set; }
        public int list_status { get; set; }
        public bool is_margin { get; set; }
        public float net_value { get; set; }
        public float net_cumulative { get; set; }
        public string tradedate { get; set; }
        public float year_yld { get; set; }
        public float tenthou_unit_incm { get; set; }
        public float percent_day { get; set; }
        public float percent_month { get; set; }
        public float percent_season { get; set; }
        public float percent_week { get; set; }
        public float percent_six_month { get; set; }
        public float percent_year { get; set; }
        public float percent_tyear { get; set; }
        public float latest_cp_rate { get; set; }
        public string end_shares { get; set; }
        public bool is_recommended { get; set; }
        public object recommend_title { get; set; }
        public object recommend_desc { get; set; }
        public bool allow_fixed { get; set; }
        public string amount_fixed_min { get; set; }
        public string amount_fixed_max { get; set; }
        public bool allow_trade { get; set; }
        public bool allow_buy { get; set; }
        public bool allow_sell { get; set; }
        public int trade_status { get; set; }
        public string fare_ratio_buy { get; set; }
        public string discount_rate_buy { get; set; }
        public string fare_ratio_sell { get; set; }
        public string discount_rate_sell { get; set; }
        public string amount_min_buy { get; set; }
        public string amount_max_buy { get; set; }
        public string shares_min_sell { get; set; }
        public string shares_max_sell { get; set; }
        public string shares_min { get; set; }
        public int investment_risk { get; set; }
        public string investment_risk_display { get; set; }
        public int charge_mode { get; set; }
        public string charge_mode_display { get; set; }
        public int t_days_sell { get; set; }
        public int company { get; set; }
        public float recommend_percent_value { get; set; }
        public string recommend_percent_display { get; set; }
        public bool is_bao { get; set; }
        public object year_yld_avg_month { get; set; }
        public object tenthou_unit_incm_avg_month { get; set; }
        public float percent_twyear { get; set; }
        public float? percent_three_year { get; set; }
        public object percent_five_year { get; set; }
        public float percent_all { get; set; }
        public int score { get; set; }
        public string analyse_description { get; set; }
        public string mana_name { get; set; }
        public string fund_name { get; set; }
        public int rank_fund_manager_index { get; set; }
        public int rank_fund_manager_total { get; set; }
        public int rank_asset_index { get; set; }
        public int rank_asset_total { get; set; }
        public string end_asset { get; set; }
        public int company_score { get; set; }
        public int asset_score { get; set; }
        public int profitability { get; set; }
        public int sharpe_ratio { get; set; }
        public int anti_risk { get; set; }
        public float? recommend_percent_fixed_value { get; set; }
        public string recommend_percent_fixed_display { get; set; }
        public int subscription_status { get; set; }
        public bool allow_subscribe { get; set; }
        public DateTime subscribe_start_date { get; set; }
        public DateTime subscribe_end_date { get; set; }
        public string amount_subscribe_min { get; set; }
        public string amount_subscribe_max { get; set; }
        public string fare_ratio_subscribe { get; set; }
        public string discount_rate_subscribe { get; set; }
        public object track_target { get; set; }
        public object track_target_name { get; set; }
        public float percent_year_fixed { get; set; }
        public float percent_twyear_fixed { get; set; }
        public float? percent_three_year_fixed { get; set; }
        public float? percent_five_year_fixed { get; set; }
        public string score_description { get; set; }
        public int stability { get; set; }
        public int excess_income { get; set; }
        public int index_following { get; set; }
        public int experience { get; set; }
        public int timing_selection { get; set; }
        public int stock_selection { get; set; }
        public bool allow_sell2bao { get; set; }
        public string trade_status_display { get; set; }
        public string bull_market { get; set; }
        public string bear_market { get; set; }
        public string seesawing_market { get; set; }
        public float org_heavy_scale { get; set; }
        public string estab_date { get; set; }
        public string company_csname { get; set; }
        public string discount_fare_ratio_buy { get; set; }
        public string fare_ratio_all { get; set; }
        public object fnd_stype { get; set; }
        public object fnd_stype_display { get; set; }
        public string asset_type_display { get; set; }
        public string style_type_display { get; set; }
        public int score_short { get; set; }
        public int score_medium { get; set; }
        public int? score_long { get; set; }
        public int recommend_period { get; set; }
        public string recommend_period_display { get; set; }
        public string recommend_period_length { get; set; }
        public int recommend_score { get; set; }
        public Fund_Manager fund_manager { get; set; }
        public Period_Analyse_Stat1[] period_analyse_stat { get; set; }
    }

    public class Fund_Manager
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar_xs { get; set; }
        public string avatar_sm { get; set; }
        public string avatar_md { get; set; }
        public string avatar_lg { get; set; }
        public string work_seniority { get; set; }
        public float win_rate_day { get; set; }
        public float win_rate_week { get; set; }
        public float win_rate_month { get; set; }
        public float win_rate_season { get; set; }
        public float win_rate_six_month { get; set; }
        public float win_rate_year { get; set; }
        public float win_rate_tyear { get; set; }
        public int manager_type { get; set; }
        public string fund_company { get; set; }
        public float index_rate_week { get; set; }
        public float index_rate_day { get; set; }
        public float index_rate_month { get; set; }
        public float index_rate_season { get; set; }
        public float index_rate_six_month { get; set; }
        public float index_rate_year { get; set; }
        public float index_rate_tyear { get; set; }
        public float index_rate_twyear { get; set; }
        public float index_rate_three_year { get; set; }
        public float index_rate_all { get; set; }
        public bool is_recommended { get; set; }
        public string recommend_title { get; set; }
        public string recommend_desc { get; set; }
        public int profitability { get; set; }
        public int stability { get; set; }
        public int anti_risk { get; set; }
        public int bull_market { get; set; }
        public int bear_market { get; set; }
        public int seesawing_market { get; set; }
        public int good_at_type { get; set; }
        public string index_rank_month { get; set; }
        public string index_rank_year { get; set; }
        public string index_rank_tyear { get; set; }
        public int ability_avg { get; set; }
        public int market_avg { get; set; }
        public string rate_date { get; set; }
        public string latest_view { get; set; }
        public int recommend_percent { get; set; }
        public float recommend_percent_value { get; set; }
        public string recommend_percent_display { get; set; }
        public float win_rate_twyear { get; set; }
        public float? win_rate_three_year { get; set; }
        public int sharpe_ratio { get; set; }
        public int score { get; set; }
        public string profitability_display { get; set; }
        public string anti_risk_display { get; set; }
        public string sharpe_ratio_display { get; set; }
        public int post_status { get; set; }
        public object end_date { get; set; }
        public float index_rate_five_year { get; set; }
        public string score_description { get; set; }
        public int excess_income { get; set; }
        public int index_following { get; set; }
        public int experience { get; set; }
        public int timing_selection { get; set; }
        public int stock_selection { get; set; }
    }

    public class Period_Analyse_Stat1
    {
        public string analyse_description { get; set; }
        public string score_description { get; set; }
        public int period { get; set; }
        public string anti_risk { get; set; }
        public string stability { get; set; }
        public string index_following { get; set; }
        public string asset_score { get; set; }
        public string experience { get; set; }
        public string sharpe_ratio { get; set; }
        public string excess_income { get; set; }
        public string period_length { get; set; }
        public string profitability { get; set; }
        public string period_display { get; set; }
        public string stock_selection { get; set; }
        public int? score { get; set; }
        public string timing_selection { get; set; }
        public string manager_description { get; set; }
    }

    public class DkhsFund_Similar_Stat
    {
        public int id { get; set; }
        public int symbol_stype { get; set; }
        public int asset_min { get; set; }
        public int asset_max { get; set; }
        public float percent { get; set; }
        public string asset_display { get; set; }
        public bool is_current_asset { get; set; }
    }

    public class DkhsFund_Profit_Loss_Predicts
    {
        public int period { get; set; }
        public string period_display { get; set; }
        public int predict_type { get; set; }
        public string predict_type_display { get; set; }
        public int range_min { get; set; }
        public int range_max { get; set; }
        public float percent { get; set; }
    }

    public class DkhsPeriod_Analyse_Stat2
    {
        public string excess_income { get; set; }
        public int score { get; set; }
        public string analyse_description { get; set; }
        public string score_description { get; set; }
        public string index_following { get; set; }
        public string period_length { get; set; }
        public int period { get; set; }
        public string experience { get; set; }
        public string anti_risk { get; set; }
        public string stability { get; set; }
        public string profitability { get; set; }
        public string stock_selection { get; set; }
        public bool allow_score { get; set; }
        public string asset_score { get; set; }
        public string period_display { get; set; }
        public string timing_selection { get; set; }
        public string sharpe_ratio { get; set; }
        public string manager_description { get; set; }
    }

}
