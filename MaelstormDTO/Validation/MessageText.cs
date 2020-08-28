using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace maelstorm_dtos.Validation
{
    public class MessageText:ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string text = value?.ToString();
            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.Trim();
                text = Regex.Replace(text, @"\s+", " ");
                if (text.Length > 1 && text.Length <= 4096)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Message's text is not valid");
        }
    }
}