using System;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace EDS.MVVM.ValidationRules
{
    public class RegularExpressionValidationRule : ValidationRule
    {
        public string RegularExpression { get; set; }

        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = ValidationResult.ValidResult;

            if (!string.IsNullOrWhiteSpace(RegularExpression))
            {
                if(!Regex.IsMatch(value?.ToString(), RegularExpression))
                {
                    validationResult = new ValidationResult(false, ErrorMessage);
                }
            }

            return validationResult;
        }
    }
}
