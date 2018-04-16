using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds.Dto
{
    /// <summary>
    /// 基金分析
    /// </summary>
    [AutoMap(typeof(Analyse))]
    public class AnalyseDto : EntityDto
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 分析描述
        /// </summary>
        [MaxLength(1024)]
        public string AnalyseDescription { get; set; }

        /// <summary>
        /// 评分描述
        /// </summary>
        [MaxLength(1024)]
        public string ScoreDescription { get; set; }

        /// <summary>
        /// 基金公司评分
        /// </summary>
        public int CompanyScore { get; set; }

        /// <summary>
        /// 资产评分
        /// </summary>
        public int AssetScore { get; set; }

        /// <summary>
        /// 盈利能力
        /// </summary>
        public int Profitability { get; set; }

        /// <summary>
        /// 夏普比率
        /// </summary>
        public int SharpeRatio { get; set; }

        /// <summary>
        /// 抗风险能力
        /// </summary>
        public int AntiRisk { get; set; }

        /// <summary>
        /// 短期评分
        /// </summary>
        public int ScoreShort { get; set; }

        /// <summary>
        /// 中期评分
        /// </summary>
        public int ScoreMedium { get; set; }

        /// <summary>
        /// 长期评分
        /// </summary>
        public int ScoreLong { get; set; }

        /// <summary>
        /// 稳定性
        /// </summary>
        public int Stability { get; set; }

        /// <summary>
        /// 超额收益
        /// </summary>
        public int ExcessIncome { get; set; }

        /// <summary>
        /// 择时能力
        /// </summary>
        public int TimingSelection { get; set; }

        /// <summary>
        /// 股票选择能力
        /// </summary>
        public int StockSelection { get; set; }

        /// <summary>
        /// 指数评分
        /// </summary>
        public int IndexFollowing { get; set; }

        /// <summary>
        /// 基金经理经验评分
        /// </summary>
        public int Experience { get; set; }
    }
}