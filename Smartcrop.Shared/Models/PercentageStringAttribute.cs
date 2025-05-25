using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SmartCrop.Shared.Models
{
    public class PercentageStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string input || string.IsNullOrWhiteSpace(input))
                return false;

            // Remove the "%" if present
            var cleaned = input.Trim().Replace("%", "");

            // Check if it's a valid number
            return double.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a number or a number followed by % (such as: 45 or 45%).";
        }
    }
}