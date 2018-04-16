using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Boss.Pim.Attributes;

namespace Boss.Pim.Funds.Dto
{
    /// <summary>
    /// 基金交易记录
    /// </summary>
    [AutoMap(typeof(TradeRecord))]
    public class TradeRecordDto : EntityDto<Guid>
    {
        public long UserId { get; set; }

        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime { get; set; }

        /// <summary>
        /// 购买时的单位净值
        /// </summary>
        public float BuyUnitNetWorth { get; set; }

        /// <summary>
        /// 购买金额
        /// </summary>
        public float BuyAmount { get; set; }
    }
}