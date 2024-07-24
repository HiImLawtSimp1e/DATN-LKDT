using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Common.Validation
{
    public class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanOrEqualToAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as int?;

            if (currentValue == null || currentValue == 0)
            {
                return ValidationResult.Success;
            }

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                return new ValidationResult($"Thuộc tính không xác định: {_comparisonProperty}");
            }

            var comparisonValue = (int?)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue.HasValue && currentValue < comparisonValue)
            {
                return new ValidationResult($"Giá gốc (nếu có) phải lớn hơn hoặc bằng giá bán.");
            }

            return ValidationResult.Success;
        }
    }
}
