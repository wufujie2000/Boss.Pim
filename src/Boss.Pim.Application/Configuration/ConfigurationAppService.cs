using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Boss.Pim.Configuration.Dto;

namespace Boss.Pim.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : PimAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
