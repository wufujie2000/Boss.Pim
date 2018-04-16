using Abp.Authorization;
using Boss.Pim.Authorization.Roles;
using Boss.Pim.Authorization.Users;

namespace Boss.Pim.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
