using System;
using System.Globalization;
using System.Windows.Controls;

namespace SymfosysCMD.Validation
{
    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty((string)value))
                return new ValidationResult(false, "The project name cannot be blank!");
            return new ValidationResult(true, null);
        }
    }
}
