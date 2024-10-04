using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class UserInformation
    {
        [Key]
        public int UserId { get; set; } = 0;

        [Required]
        [Display(Name = "Full Name")]
        public string FName { get; set; } = default!;

        [Required]
        [Display(Name = "Mobile Number")]
        public long MobileNumber { get; set; } = default!;

        [Required]
        [Display(Name = "Aadhar Number")]
        public long AadharNumber { get; set; } = default!;

        [Required]
        [Display(Name = "Date-Of-Birth")]
        public DateTime DateOfBirth { get; set; } = default!;

        public int RegistrationId { get; set; }
        [ForeignKey("RegistrationId")]
        public Registration Registration { get; set; } = default!;

        public ICollection<EducationDetails> EducationDetails { get; set; } = default!;

        public ICollection<UserAddressDetails> UserAddressDetails { get; set; } = default!;
        public ICollection<TechnologyDetails> TechnologyDetails { get; set; } = default!;
    }
}
