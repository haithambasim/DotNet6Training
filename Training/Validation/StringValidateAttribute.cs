using System.ComponentModel.DataAnnotations;

namespace Training.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class StringValidateAttribute : ValidationAttribute
    {
        int _min, _max; bool _null;

        public StringValidateAttribute(int Min = -1, int Max = -1, bool Null = false)
        {
            _min = Min; _max = Max; _null = Null;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_null && value == null)
                return ValidationResult.Success;

            if (value == null || string.IsNullOrWhiteSpace((string)value))
                return new ValidationResult("Required");

            if (_min != -1 && ((string)value).Length < _min)
                return new ValidationResult(string.Format("MinimumStringLengthAllowedIs", _min));

            if (_max != -1 && ((string)value).Length > _max)
                return new ValidationResult(string.Format("MaximumStringLengthAllowedIs", _max));

            return ValidationResult.Success;
        }
    }
}
