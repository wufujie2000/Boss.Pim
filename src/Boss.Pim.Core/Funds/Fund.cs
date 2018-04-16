using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    public class Fund : AuditedAggregateRoot
    {
        public const int CodeLength = 128;
        public const int ShortNameLength = 512;

        /// <summary>
        /// 基金代码
        /// </summary>
        [MaxLength(CodeLength)]
        public string Code { get; set; }

        /// <summary>
        /// 基金决策宝Code
        /// </summary>
        [MaxLength(CodeLength)]
        public string DkhsCode { get; set; }

        /// <summary>
        /// 基金简称
        /// </summary>
        [MaxLength(ShortNameLength)]
        public string ShortName { get; set; }

        /// <summary>
        /// 基金全称
        /// </summary>
        [MaxLength(1024)]
        public string Name { get; set; }

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

        /// <summary>
        /// 基金托管人
        /// </summary>
        [MaxLength(256)]
        public string TrupName { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>
        public DateTime? EstabDate { get; set; }
    }
}