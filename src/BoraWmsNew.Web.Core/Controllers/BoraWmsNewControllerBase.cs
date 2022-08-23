using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BoraWmsNew.Controllers
{
    public abstract class BoraWmsNewControllerBase: AbpController
    {
        protected BoraWmsNewControllerBase()
        {
            LocalizationSourceName = BoraWmsNewConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
