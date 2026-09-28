using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public class MaxDecimalPrecisionAttribute : ValidationAttribute
    {
        private readonly int _maxPrecision;

        public MaxDecimalPrecisionAttribute(int maxPrecision) 
        {
            _maxPrecision = maxPrecision;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if(value is null)
            {
                return ValidationResult.Success;
            }

            if (value is decimal)
            {
                var dec = (decimal)value;
                var rounded = decimal.Round(dec, _maxPrecision);
                if (dec != rounded)
                {
                    return new ValidationResult(base.ErrorMessage ?? $"{ctx.DisplayName} must not contain more than {_maxPrecision} decimal places.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
