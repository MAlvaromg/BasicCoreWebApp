using System.ComponentModel.DataAnnotations;

namespace Application
{
    public class GreaterOrEqualsThanAttribute : ValidationAttribute
    {
        private readonly int minValue;
        public GreaterOrEqualsThanAttribute(int minValue)
        {
            this.minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var parsedValue = 0;
            if (int.TryParse(value.ToString(), out parsedValue) && parsedValue < minValue)
            {
                return new ValidationResult($"{validationContext.MemberName} must be greater or equal than {minValue}");
            }
            return null;
        }
    }


}