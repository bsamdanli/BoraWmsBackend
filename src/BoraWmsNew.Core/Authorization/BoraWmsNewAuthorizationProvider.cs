using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace BoraWmsNew.Authorization
{
    public class BoraWmsNewAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Client, L("Client"));
            context.CreatePermission(PermissionNames.Pages_Product, L("Product"));
            context.CreatePermission(PermissionNames.Pages_Storage, L("Storage"));
            context.CreatePermission(PermissionNames.Pages_Receiving, L("Receiving"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoraWmsNewConsts.LocalizationSourceName);
        }
    }
}
