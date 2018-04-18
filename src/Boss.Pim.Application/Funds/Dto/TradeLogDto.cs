using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Boss.Pim.Funds.ObjectValues;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(TradeLog))]
    public class TradeLogDto : EntityDto<Guid>
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}