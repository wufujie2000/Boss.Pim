using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.Roles.Dto;

namespace Boss.Pim.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
