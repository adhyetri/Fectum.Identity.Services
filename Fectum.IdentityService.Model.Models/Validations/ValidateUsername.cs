using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Validations
{
    public partial class ValidateUsername : ValidationAttribute
    {
        private const string RegexPattern = @"^(?=.*[A-Z])(?=.*[a-z])[A-Za-z0-9]{1,16}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string str || !Regex.IsMatch(str, RegexPattern))
            {
                return new ValidationResult("Username must contain only one uppercase character, lowercase characters, and numbers, no special characters or spaces, and be 16 characters or less.");
            }

            return ValidationResult.Success;
        }
    }
}
