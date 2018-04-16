using System.Threading.Tasks;
using Abp.Application.Services;
using Boss.Pim.Configuration.Dto;

namespace Boss.Pim.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}