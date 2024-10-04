using Fectum.IdentityService.Model.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class Credentials
    {
        [Required]
        [ValidateUsername]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
