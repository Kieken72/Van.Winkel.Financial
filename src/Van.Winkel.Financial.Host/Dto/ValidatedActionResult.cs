using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Van.Winkel.Financial.Service.Validation;
using Error = Van.Winkel.Financial.Contracts.Validation.Error;
using ValidationBag = Van.Winkel.Financial.Contracts.Validation.ValidationBag;

namespace Van.Winkel.Financial.Host.Dto
{
    public class ValidatedActionResult<T> : IConvertToActionResult
    {
        private readonly BaseResponse _response;
        private readonly T _data;

        public ValidatedActionResult(BaseResponse response, T data)
        {
            _response = response;
            _data = data;
        }

        public IActionResult Convert()
        {
            if (_response.IsValid)
            {
                return new OkObjectResult(_data);
            }

            return new BadRequestObjectResult(
                new ValidationBag(_response.Bag.Errors.Select(e =>
                    new Error(e.ValidationErrorCode, e.NamedParameters))));
        }
    }

    public static class ConvertValidatedActionResult
    {
        public static IActionResult ToActionResult<TResponse, TData>(this TResponse response,
            Func<TResponse, TData> selector) where TResponse : BaseResponse
        {
            return new ValidatedActionResult<TData>(response, selector(response)).Convert();
        }
    }
}
