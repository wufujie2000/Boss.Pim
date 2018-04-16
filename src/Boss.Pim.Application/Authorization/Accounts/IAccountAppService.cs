using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Authorization.Accounts.Dto;

namespace Boss.Pim.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
