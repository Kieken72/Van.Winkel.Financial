using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Van.Winkel.Financial.Enums;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Service.Validation;

namespace Van.Winkel.Financial.Service.Customer
{
    public class UpdateCustomerRequest : IRequest<UpdateCustomerResponse>
    {
        public Contracts.Customer Customer { get; set; }
    }

    public class UpdateCustomerResponse : BaseResponse
    {
        public Contracts.Customer Customer { get; set; }
    }

    public class UpdateCustomerRequestValidator : RequestValidator<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public UpdateCustomerRequestValidator(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }

        protected override async Task<ValidationBag> Validate(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var bag = new ValidationBag();

            var customer = await _financialContext.Customers.FindAsync(request.Customer.Id);
            if(customer==null)
                bag.AddError(ValidationErrorCode.CustomerNotFound, new { request.Customer.Id });

            if (request.Customer.Name.Length > 250)
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

    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        private readonly IFinancialContext _financialContext;

        public UpdateCustomerRequestHandler(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _financialContext.Customers.FindAsync(request.Customer.Id);

            customer.ChangeName(request.Customer.Name, request.Customer.Surname);
            await _financialContext.SaveChangesAsync(cancellationToken);

            return new UpdateCustomerResponse
            {
                Customer = new Contracts.Customer
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname
                }
            };
        }
    }
}
