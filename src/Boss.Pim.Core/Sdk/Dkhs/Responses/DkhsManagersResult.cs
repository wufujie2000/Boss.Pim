using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.Pim.Sdk.Dkhs.Responses
{
    public class DkhsManagersResult
    {
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int manager_type { get; set; }
        /// <summary>
        /// 头像 80*80
        /// </summary>
        public string avatar_xs { get; set; }
        /// <summary>
        /// 头像 160*160
        /// </summary>
        public string avatar_sm { get; set; }
        /// <summary>
        /// 头像 320*320
        /// </summary>
        public string avatar_md { get; set; }
        /// <summary>
        /// 头像 640*640
        /// </summary>
        public string avatar_lg { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string work_seniority { get; set; }
        public float win_rate_day { get; set; }
        public float win_rate_week { get; set; }
        public float win_rate_month { get; set; }
        public float win_rate_season { get; set; }
        public float? win_rate_six_month { get; set; }
        public float? win_rate_year { get; set; }
        public float? win_rate_tyear { get; set; }
        /// <summary>
        /// 两年收益率
        /// </summary>
        public float? win_rate_twyear { get; set; }
        /// <summary>
        /// 基金公司
        /// </summary>
        public string fund_company { get; set; }
        public float index_rate_day { get; set; }
        public float index_rate_week { get; set; }
        public float index_rate_month { get; set; }
        public float index_rate_season { get; set; }
        public float index_rate_six_month { get; set; }
        public float index_rate_year { get; set; }
        public float index_rate_tyear { get; set; }
        public float index_rate_twyear { get; set; }
        public float index_rate_all { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool is_recommended { get; set; }
        /// <summary>
        /// 推荐标题
        /// </summary>
        public string recommend_title { get; set; }
        /// <summary>
        /// 推荐描述
        /// </summary>
        public string recommend_desc { get; set; }
        /// <summary>
        /// 公司编码
        /// </summary>
        public int company { get; set; }
        public DkhsTag[] tags { get; set; }
        public int recommend_percent { get; set; }
        public float recommend_percent_value { get; set; }
        public string recommend_percent_display { get; set; }
        public float? win_rate_three_year { get; set; }
        public float index_rate_three_year { get; set; }
        public float index_rate_five_year { get; set; }

        /// <summary>
        /// 擅长类型
        /// </summary>
        public int good_at_type { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public int score { get; set; }
        public float org_heavy_scale { get; set; }
        public string job_hopping { get; set; }
        public int funds_num { get; set; }
        public string bull_market { get; set; }
        public string bear_market { get; set; }
        public string seesawing_market { get; set; }

        /// <summary>
        /// 擅长类型名称
        /// </summary>
        public string good_at_type_display { get; set; }

        /// <summary>
        /// 短期评分
        /// </summary>
        public int score_short { get; set; }
        /// <summary>
        /// 中期评分
        /// </summary>
        public int score_medium { get; set; }
        /// <summary>
        /// 长期评分
        /// </summary>
        public int score_long { get; set; }
        public bool following { get; set; }
        public bool is_alert { get; set; }
        public object alert_settings { get; set; }

        /// <summary>
        /// 条件标签
        /// </summary>
        public DkhsCondition_Tags[] condition_tags { get; set; }
    }
}
