using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Domain;
using Van.Winkel.Financial.Enums;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Service.Validation;

namespace Van.Winkel.Financial.Service.Account
{
    public class AddAccountRequest : IRequest<AddAccountResponse>
    {
        public Guid CustomerId { get; set; }
        public decimal InitialCredit { get; set; }
    }

    public class AddAccountResponse : BaseResponse
    {
        public Contracts.Account Account { get; set; }
    }

    public class AddAccountRequestValidator : RequestValidator<AddAccountRequest, AddAccountResponse>
    {
        private readonly IFinancialContext _financialContext;

        public AddAccountRequestValidator(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        protected override async Task<ValidationBag> Validate(AddAccountRequest request, CancellationToken cancellationToken)
        {
            var bag = new ValidationBag();

            if (!await _financialContext.Customers.AnyAsync(_ => _.Id == request.CustomerId, cancellationToken))
            {
                bag.AddError(ValidationErrorCode.CustomerNotFound);
            }

            if (decimal.Compare(request.InitialCredit,0)<0)
                bag.AddError(ValidationErrorCode.InvalidUnderZeroInitialCredit, new { request.InitialCredit });

            return bag;
        }
    }

    public class AddAccountRequestHandler : IRequestHandler<AddAccountRequest, AddAccountResponse>
    {
        private readonly IFinancialContext _financialContext;

        public AddAccountRequestHandler(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        public async Task<AddAccountResponse> Handle(AddAccountRequest request, CancellationToken cancellationToken)
        {
            var accountToAdd = new Domain.Account(request.CustomerId);


            var result = await _financialContext.Accounts.AddAsync(accountToAdd, cancellationToken);

            if (request.InitialCredit != 0)
            {
                accountToAdd.UpdateBalance(request.InitialCredit);
                var transactionToAdd = new Transaction(result.Entity.Id, null, request.InitialCredit, "Initial transaction");
                await _financialContext.Transactions.AddAsync(transactionToAdd, cancellationToken);
            }

            await _financialContext.SaveChangesAsync(cancellationToken);
            return new AddAccountResponse
            {
                Account = new Contracts.Account
                {
                    CustomerId = result.Entity.CustomerId,
                    Balance = result.Entity.Balance
                }
            };
        }
    }
}
