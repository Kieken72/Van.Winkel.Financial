using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Van.Winkel.Financial.Enums;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Service.Validation;

namespace Van.Winkel.Financial.Service.Customer
{
    public class AddCustomerRequest : IRequest<AddCustomerResponse>
    {
        public Contracts.Customer Customer { get; set; }
    }

    public class AddCustomerResponse : BaseResponse
    {
        public Contracts.Customer Customer { get; set; }
    }

    public class AddCustomerRequestValidator : RequestValidator<AddCustomerRequest, AddCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public AddCustomerRequestValidator(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        protected override async Task<ValidationBag> Validate(AddCustomerRequest request, CancellationToken cancellationToken)
        {
            var bag = new ValidationBag();

            if(request.Customer.Name.Length > 250)
                bag.AddError(ValidationErrorCode.InvalidMaxNameLength, new { request.Customer.Name});
            if (request.Customer.Surname.Length > 250)
                bag.AddError(ValidationErrorCode.InvalidMaxSurnameLength, new { request.Customer.Surname });
            if (string.IsNullOrWhiteSpace(request.Customer.Name))
                bag.AddError(ValidationErrorCode.InvalidMinNameLength, new { request.Customer.Name });
            if (string.IsNullOrWhiteSpace(request.Customer.Surname))
                bag.AddError(ValidationErrorCode.InvalidMinSurnameLength, new { request.Customer.Surname });

            return bag;
        }
    }

    public class AddCustomerRequestHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public AddCustomerRequestHandler(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
        {
            var customerToAdd = new Domain.Customer(request.Customer.Name, request.Customer.Surname);

            var result = await _financialContext.Customers.AddAsync(customerToAdd, cancellationToken);
            await _financialContext.SaveChangesAsync(cancellationToken);
            return new AddCustomerResponse
            {
                Customer = new Contracts.Customer
                {
                    Id = result.Entity.Id,
                    Name = result.Entity.Name,
                    Surname = result.Entity.Surname
                }
            };
        }
    }
}
