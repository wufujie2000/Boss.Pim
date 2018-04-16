using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Sessions.Dto;

namespace Boss.Pim.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
