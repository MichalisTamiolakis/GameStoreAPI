using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Services
{
    public class DecimalScaleAttribute : ValidationAttribute
    {
        private readonly int _minScale;
        private readonly int _maxScale;

        private string? _errorMessage;

        public DecimalScaleAttribute(int max, int min = 0, string? errorMessage = null) 
        {
            _minScale = min;
            _maxScale = max;
            _errorMessage = errorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if(value is null || value is not decimal)
            {
                return ValidationResult.Success;
            }

            var dec = (decimal)value;

            if(dec.Scale > _maxScale)
            {
                return new ValidationResult(_errorMessage ?? $"{ctx.DisplayName} must not contain more than {_maxScale} decimal places.");
            }

            if (dec.Scale < _minScale) 
            { 
                return new ValidationResult(_errorMessage ?? $"{ctx.DisplayName} must not contain less than {_minScale} decimal places.");
            }

            return ValidationResult.Success;
        }
    }
}
