using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace maelstorm_dtos.Validation
{
    public class Nickname: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var regexItem = new Regex("^[a-zA-Z0-9]+$");
            string str = (string)value;
            if (!string.IsNullOrWhiteSpace(str))
                if (regexItem.IsMatch(str) && (str.IndexOf(' ') == -1))
                    return ValidationResult.Success;
            return new ValidationResult("Nickname can include only letters and numbers");
        }
    }
}