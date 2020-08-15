using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Van.Winkel.Financial.Enums;

namespace Van.Winkel.Financial.Service.Validation
{
    public class ValidationBag
    {
        private readonly ValidationErrorCode[] NotFoundErrors = new ValidationErrorCode[]
        {
            ValidationErrorCode.CustomerNotFound
        };


        private readonly List<Error> _errors;
        public IEnumerable<Error> Errors => _errors.AsEnumerable();

        public ValidationBag()
        {
            _errors = new List<Error>();
        }

        public void AddError(ValidationErrorCode error)
        {
            _errors.Add(new Error(error));
        }

        public void AddError(ValidationErrorCode error, object parameters)
        {
            _errors.Add(new Error(error, parameters));
        }

        public bool IsValid => !EnumerableExtensions.Any(_errors);
        public bool HasNotFoundError => _errors.Any(x => this.NotFoundErrors.Contains(x.ValidationErrorCode));
    }
}