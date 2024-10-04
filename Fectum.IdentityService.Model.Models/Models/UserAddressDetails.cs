using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fectum.IdentityService.Model.Models.Enums;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class UserAddressDetails
    {
        [Key]
        public int AddressId { get; set; } = 0;
        public AddressType AddressType { get; set; }
        public CountryName Country { get; set; }
        public string StreetAddress { get; set; } = default!;
        public string CityName { get; set; } = default!;
        public string StateName { get; set; } = default!;
        public int PostalCode { get; set; } = default!;

        [ForeignKey(nameof(UserInformation.UserId))]
        public int UserId { get; set; } = default!;
        public UserInformation UserInformation { get; set; } = default!;
        public bool IsSameAsPermanent { get; set; } = false;
    }
}
