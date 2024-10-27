using Fectum.IdentityService.Model.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class RegistrationDTO
    {
        public int RegistrationId { get; set; } = 0;

        [Required]
        [ValidateUsername]
        public string Username { get; set; } = default!;

        [Required]
        public string EmailAddress { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        public UserInformationDTO UserInformation { get; set; } = default!;
    }
}
