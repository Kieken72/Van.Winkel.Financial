using System.Collections.Generic;
using System.Linq;

namespace Van.Winkel.Financial.Contracts.Validation
{
    public class ValidationBag
    {
        public IEnumerable<Error> Errors { get; }
        public bool IsValid => !Errors.Any();


        public ValidationBag(IEnumerable<Error> errors)
        {
            Errors = errors;
        }
    }
}
