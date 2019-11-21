using System;
using System.Globalization;
using System.Windows.Controls;

namespace EDS.MVVM.ValidationRules
{
    public class RequiredFieldValidationRule : ValidationRule
    {

        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = ValidationResult.ValidResult;

            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                validationResult = new ValidationResult(false, ErrorMessage);
            }

            return validationResult;
        }
    }
}
