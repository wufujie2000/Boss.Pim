using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Boss.Pim.Funds.Dto
{
    [AutoMap(typeof(FundAllocation))]
    public class FundAllocationDto : EntityDto
    {
    }
}
