using Abp.Authorization;
using BoraWmsNew.Authorization.Roles;
using BoraWmsNew.Authorization.Users;

namespace BoraWmsNew.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
