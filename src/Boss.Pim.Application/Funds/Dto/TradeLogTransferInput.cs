using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(TradeLog))]
    public class TradeLogTransferInput : EntityDto<Guid>
    {
        /// <summary>
        /// 转出基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FromFundCode { get; set; }

        /// <summary>
        /// 转入基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string ToFundCode { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 交易份额
        /// </summary>
        public float Portion { get; set; }

        /// <summary>
        /// 转出费率
        /// </summary>
        public float FromServiceRate { get; set; }

        /// <summary>
        /// 转入费率
        /// </summary>
        public float ToServiceRate { get; set; }
    }
}