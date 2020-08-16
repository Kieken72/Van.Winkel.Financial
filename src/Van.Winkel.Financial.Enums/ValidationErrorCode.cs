using System;

namespace Van.Winkel.Financial.Enums
{
    public enum ValidationErrorCode
    {
        InvalidMaxNameLength = 1,
        InvalidMaxSurnameLength = 2,
        InvalidMinNameLength = 3,
        InvalidMinSurnameLength = 4,
        CustomerNotFound = 5,
        InvalidUnderZeroInitialCredit = 6,
    }
}
