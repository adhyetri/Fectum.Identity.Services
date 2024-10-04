using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Models
{
    public class TechnologyDetails
    {
        public int Id { get; set; }
        private string[] _technologies = { "", "", "", "", "", "" };

        public string[] Technologies 
        {
            set { _technologies = value; }
            get { return _technologies; }
            
        }
        public int UserId { get; set; } = default!;
        public UserInformation UserInformation { get; set; } = default!;
    }
}
