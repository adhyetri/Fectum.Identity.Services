using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class EducationDetails
    {
        [Key]
        public int EducationId { get; set; }
        public string DegreeName { get; set; } = default!;
        public string UniversityName { get; set; } = default!;
        public string DepartmentName { get; set; } = default!;
        public string CompletionYear { get; set; } = default!;

        [ForeignKey(nameof(UserInformation.UserId))]
        public int UserId { get; set; } = default!;
        public UserInformation UserInformation { get; set; } = default!;
    }
}
