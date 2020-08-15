using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Enums;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Service.Validation;

namespace Van.Winkel.Financial.Service.Customer
{
    public class GetCustomerRequest : IRequest<GetCustomerResponse>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetCustomerResponse : BaseResponse
    {
        public Contracts.Customer Customer { get; set; }
    }

    public class GetCustomerRequestValidator : RequestValidator<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public GetCustomerRequestValidator(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }

        protected override async Task<ValidationBag> Validate(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var bag = new ValidationBag();

            if (!await _financialContext.Customers.AnyAsync(_ => _.Id == request.CustomerId, cancellationToken))
            {
                bag.AddError(ValidationErrorCode.CustomerNotFound);
            }

            return bag;
        }
    }

    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public GetCustomerRequestHandler(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _financialContext.Customers/*.Include(_ => _.Accounts).ThenInclude(_ => _.Transactions)*/.SingleAsync(_ => _.Id == request.CustomerId, cancellationToken);

            return new GetCustomerResponse
            {
                Customer = new Contracts.Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname
                    
                }
            };
        }
    }
}
