using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BoraWmsNew.Configuration.Dto;

namespace BoraWmsNew.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BoraWmsNewAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
