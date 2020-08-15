using Van.Winkel.Financial.Enums;

namespace Van.Winkel.Financial.Contracts.Validation
{
    public class Error
    {
        public ValidationErrorCode ValidationErrorCode { get; }
        public object NamedParameters { get; }

        public Error(ValidationErrorCode validationErrorCode, object namedParameters)
        {
            ValidationErrorCode = validationErrorCode;
            NamedParameters = namedParameters;
        }
    }
}