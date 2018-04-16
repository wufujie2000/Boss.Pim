using Abp.AutoMapper;
using Boss.Pim.Sessions.Dto;

namespace Boss.Pim.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}