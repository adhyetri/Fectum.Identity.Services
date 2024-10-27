using Fectum.IdentityService.Model.Models.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; } = 0;

        [Required]
        [ValidateUsername]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [SkipValidationInStep]
        public UserInformation UserInformation { get; set; } = default!;
    }
}
