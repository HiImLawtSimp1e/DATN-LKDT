using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Common.Validation
{
    public class DateGreaterThan : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThan(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate <= startDate)
            {
                return new ValidationResult("Ngày hết hạn voucher không được sớm hơn ngày bắt đầu áp dụng voucher");
            }

            return ValidationResult.Success;
        }
    }
}
