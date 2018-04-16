using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金评级 汇总
    /// </summary>
    public class RatingPool : AuditedAggregateRoot
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 海通证券 评级 3年期
        /// </summary>
        public int HtsecRating3 { get; set; }

        /// <summary>
        /// 海通证券 评级 5年期
        /// </summary>
        public int HtsecRating5 { get; set; }

        /// <summary>
        /// 招商证券 评级 3年期
        /// </summary>
        public int ZssecRating3 { get; set; }

        /// <summary>
        /// 招商证券 评级 5年期
        /// </summary>
        public int ZssecRating5 { get; set; }

        /// <summary>
        /// 上海证券评级 3年期
        /// </summary>
        public int ShsecRating3 { get; set; }

        /// <summary>
        /// 上海证券评级 5年期
        /// </summary>
        public int ShsecRating5 { get; set; }

        /// <summary>
        /// 济安金信 评级 3年期
        /// </summary>
        public int JajxRating3 { get; set; }

        /// <summary>
        /// 济安金信 评级 5年期
        /// </summary>
        public int JajxRating5 { get; set; }

        /// <summary>
        /// 晨星网 评级 3年期
        /// </summary>
        public int MstarRating3 { get; set; }

        /// <summary>
        /// 晨星网 评级 5年期
        /// </summary>
        public int MstarRating5 { get; set; }

        /// <summary>
        /// 银河证券 评级 3年期
        /// </summary>
        public int GalaxyRating3 { get; set; }

        /// <summary>
        /// 银河证券 评级 5年期
        /// </summary>
        public int GalaxyRating5 { get; set; }

        /// <summary>
        /// 天相投顾 评级 3年期
        /// </summary>
        public int TxsecRating3 { get; set; }

        /// <summary>
        /// 天相投顾 评级 5年期
        /// </summary>
        public int TxsecRating5 { get; set; }
    }
}