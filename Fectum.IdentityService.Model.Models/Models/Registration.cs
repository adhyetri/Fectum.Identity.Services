using Fectum.IdentityService.Model.Models.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationId { get; set; } = 0;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        [ComparePassword("Password")]
        public string ConfirmPassword { get; set; } = default!;

        [Required]
        [ValidateUsername]
        public string Username { get; set; } = default!;

        [SkipValidationInStep]
        public UserInformation UserInformation { get; set; } = default!;

        public int RoleId { get; set; }

        [SkipValidationInStep]
        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; } = default!;
    }
}
