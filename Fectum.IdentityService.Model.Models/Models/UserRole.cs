using Fectum.IdentityService.Model.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; } = 0;

        [Required]
        public UserRoleType RoleName { get; set; } = default!;
    }
}
