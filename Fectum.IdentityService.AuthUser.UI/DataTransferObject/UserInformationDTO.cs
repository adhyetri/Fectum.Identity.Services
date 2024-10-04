using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class UserInformationDTO
    {
        public int UserId { get; set; } = 0;

        public string FName { get; set; } = default!;

        public long MobileNumber { get; set; } = default!;

        public long AadharNumber { get; set; } = default!;

        public DateTime DateOfBirth { get; set; } = default!;

        public int RegistrationId { get; set; } = 0;
    }
}
