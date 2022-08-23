using System.Threading.Tasks;
using BoraWmsNew.Configuration.Dto;

namespace BoraWmsNew.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
