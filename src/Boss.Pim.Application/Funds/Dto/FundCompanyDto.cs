using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace Boss.Pim.Funds.Dto
{
    /// <summary>
    /// 基金公司
    /// </summary>
    [AutoMap(typeof(FundCompany))]
    public class FundCompanyDto : EntityDto
    {
        public const int NameLength = 256;

        /// <summary>
        /// 公司名称
        /// </summary>
        [MaxLength(NameLength)]
        public string Name { get; set; }
    }
}
