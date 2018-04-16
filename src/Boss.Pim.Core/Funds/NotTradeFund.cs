using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    public class NotTradeFund : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 基金编码
        /// </summary>
        [Required]
        [MaxLength(Fund.CodeLength)]
        public string FundCode { get; set; }

        /// <summary>
        /// 平台
        /// </summary>
        [MaxLength(256)]
        public string SourcePlatform { get; set; }
    }
}
