using System.ComponentModel.DataAnnotations.Schema;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class EducationDetailsDTO
    {
        public int EducationId { get; set; }
        public string DegreeName { get; set; } = default!;
        public string UniversityName { get; set; } = default!;
        public string DepartmentName { get; set; } = default!;
        public string CompletionYear { get; set; } = default!;
        public int UserId { get; set; } = default!;
    }
}
