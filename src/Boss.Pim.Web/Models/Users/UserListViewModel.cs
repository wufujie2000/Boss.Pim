using System.Collections.Generic;
using Boss.Pim.Roles.Dto;
using Boss.Pim.Users.Dto;

namespace Boss.Pim.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}