using System.ComponentModel.DataAnnotations;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class UserRoleDTO
    {
        public int RoleId { get; set; } = 0;

        public string RoleName { get; set; } = default!;
    }
}
