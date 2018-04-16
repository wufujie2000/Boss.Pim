using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(Fund))]
    public class FundDto : EntityDto
    {
        public const int CodeLength = 128;
        public const int ShortNameLength = 512;

        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(CodeLength)]
        public string Code { get; set; }

        /// <summary>
        /// 基金简称
        /// </summary>
        [MaxLength(ShortNameLength)]
        public string ShortName { get; set; }

        /// <summary>
        /// 名称首字母
        /// </summary>
        [MaxLength(ShortNameLength)]
        public string ShortNameInitials { get; set; }

        /// <summary>
        /// 名称拼音
        /// </summary>
        [MaxLength(ShortNameLength)]
        public string ShortNamePinYin { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(ShortNameLength)]
        public string TypeName { get; set; }

        /// <summary>
        /// 是否自选
        /// </summary>
        public bool IsOptional { get; set; }
    }
}