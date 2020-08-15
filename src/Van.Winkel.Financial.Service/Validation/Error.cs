using Van.Winkel.Financial.Enums;

namespace Van.Winkel.Financial.Service.Validation
{
    public class Error
    {
        public ValidationErrorCode ValidationErrorCode { get; }
        public object NamedParameters { get; }
        public Error(ValidationErrorCode validationErrorCode)
        {
            ValidationErrorCode = validationErrorCode;
        }
        public Error(ValidationErrorCode validationErrorCode, object namedParameters) : this(validationErrorCode)
        {
            NamedParameters = namedParameters;
        }
    }
}