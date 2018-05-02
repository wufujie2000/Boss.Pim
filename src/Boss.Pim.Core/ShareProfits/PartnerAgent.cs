using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.ShareProfits
{
    /// <summary>
    /// 合伙人 代理
    /// </summary>
    public class PartnerAgent : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 管理用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 拥有产品数量
        /// </summary>
        public int ProductQty { get; set; }
    }
}
