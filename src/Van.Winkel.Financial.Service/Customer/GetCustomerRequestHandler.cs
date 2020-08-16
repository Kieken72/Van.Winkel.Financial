using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Contracts;
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
        public Contracts.CustomerWithAccount Customer { get; set; }
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
            var customer = await _financialContext.Customers.Include(_ => _.Accounts).ThenInclude(_ => _.IncomingTransactions).SingleAsync(_ => _.Id == request.CustomerId, cancellationToken);

            return new GetCustomerResponse
            {
                Customer = new Contracts.CustomerWithAccount
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    Accounts = customer.Accounts.Select(_=> new AccountWithTransactions
                    {
                        Id = _.Id,
                        Balance = _.Balance,
                        Transactions = _.IncomingTransactions.Select(t=> new Transaction
                        {
                            SenderAccountId = t.SenderAccountId,
                            Amount = t.Amount,
                            Note = t.Note

                        })
                    })
                    
                }
            };
        }
    }
}
