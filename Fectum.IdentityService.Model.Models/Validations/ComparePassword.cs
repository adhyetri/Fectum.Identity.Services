using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fectum.IdentityService.Model.Models.Validations
{
    public class ComparePassword(string password) : ValidationAttribute
    {
        private readonly string _password = password;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? currentValue = value as string;

            PropertyInfo? obj = validationContext.ObjectType.GetProperty(_password);

            if (obj == null) { return new ValidationResult($"Unknown property: {_password}"); }

            string? compareValue = obj.GetValue(validationContext.ObjectInstance) as string;

            if (!string.Equals(currentValue, compareValue))
            {
                return new ValidationResult("Password and Confirmation Password did not match.");
            }

            return ValidationResult.Success;
        }
    }
}
