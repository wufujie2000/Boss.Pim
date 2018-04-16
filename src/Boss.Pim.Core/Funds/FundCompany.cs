using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds
{
    /// <summary>
    /// 基金公司
    /// </summary>
    public class FundCompany : AuditedAggregateRoot
    {
        public const int NameLength = 256;

        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(NameLength)]
        public string Name { get; set; }
    }
}
