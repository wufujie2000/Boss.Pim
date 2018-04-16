using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Boss.Pim.Roles.Dto;
using Boss.Pim.Users.Dto;

namespace Boss.Pim.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}