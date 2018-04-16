using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds.Dto
{
    /// <summary>
    /// 基金经理
    /// </summary>
    [AutoMap(typeof(FundManager))]
    public class FundManagerDto : EntityDto
    {
        public const int NameLength = 128;

        /// <summary>
        /// 基金公司Id
        /// </summary>
        public int FundCompanyId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(NameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 上任日期
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [MaxLength(2048)]
        public string Introduction { get; set; }

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
        /// 综合评分
        /// </summary>
        public int score { get; set; }
    }
}