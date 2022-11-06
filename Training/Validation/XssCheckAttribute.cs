using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using Training.Exceptions;

namespace Training.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class XssCheckAttribute : ValidationAttribute
    {
        public XssCheckAttribute()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace((string)value) && !ValidateAntiXSS((string)value))
            {
                //return new ValidationResult("Contains harmful text");
                throw new UserFriendlyException("Contains harmful text");
            }

            return ValidationResult.Success;
        }

        private static bool ValidateAntiXSS(string inputParameter)
        {
            if (string.IsNullOrEmpty(inputParameter))
                return true;

            var pattren = new StringBuilder();

            //Checks any js events i.e. onKeyUp(), onBlur(), alerts and custom js functions etc.             
            pattren.Append(@"((alert|on\w+|function\s+\w+)\s*\(\s*(['+\d\w](,?\s*['+\d\w]*)*)*\s*\))");

            //Checks any html tags i.e. <script, <embed, <object etc.
            pattren.Append(@"|(<(script|iframe|embed|frame|frameset|object|img|applet|body|html|style|layer|link|ilayer|meta|bgsound))");

            return !Regex.IsMatch(System.Web.HttpUtility.UrlDecode(inputParameter), pattren.ToString(), RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
