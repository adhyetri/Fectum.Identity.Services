using Fectum.IdentityService.Model.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class UserRoleDTO
    {
        public int RoleId { get; set; } = 0;

        public UserRoleType RoleName { get; set; } = default!;
        
        public bool IsWorkingProffesional = true;
    }
}
