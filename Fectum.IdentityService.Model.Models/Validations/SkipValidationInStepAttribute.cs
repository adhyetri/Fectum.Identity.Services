using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Validations
{
    public class SkipValidationInStepAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return true;
        }
    }
}
