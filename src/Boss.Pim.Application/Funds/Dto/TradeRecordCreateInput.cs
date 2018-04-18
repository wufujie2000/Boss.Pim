using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Boss.Pim.Funds.ObjectValues;

namespace Boss.Pim.Funds.Dto
{
    public class TradeRecordCreateInput : EntityDto<Guid>
    {
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

        /// <summary>
        /// 确认金额
        /// </summary>
        public float ConfirmAmount { get; set; }

        /// <summary>
        /// 确认份额
        /// </summary>
        public float ConfirmShare { get; set; }

        /// <summary>
        /// 购买手续费
        /// </summary>
        public float BuyServiceCharge { get; set; }

        /// <summary>
        /// 交易记录类型
        /// </summary>
        public TradeRecordType TradeRecordType { get; set; }
    }
}
