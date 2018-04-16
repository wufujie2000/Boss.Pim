using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.MultiTenancy.Dto;

namespace Boss.Pim.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
