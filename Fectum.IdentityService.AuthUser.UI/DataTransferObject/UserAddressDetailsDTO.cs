using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Fectum.IdentityService.Model.Models.Enums;

namespace Fectum.IdentityService.AuthUser.UI.DataTransferObject
{
    public class UserAddressDetailsDTO
    {
        public int AddressId { get; set; } = 0;
        public AddressType AddressType { get; set; }
        public CountryName Country { get; set; }
        public string StreetAddress { get; set; } = default!;
        public string CityName { get; set; } = default!;
        public string StateName { get; set; } = default!;
        public int PostalCode { get; set; } = default!;
        public int UserId { get; set; } = default!;
        public bool IsSameAsPermanent { get; set; } = false;
    }
}
