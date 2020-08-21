using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Van.Winkel.Financial.Infrastructure.EntityFramework;
using Van.Winkel.Financial.Service.Validation;

namespace Van.Winkel.Financial.Service.Customer
{
    public class GetCustomersRequest : IRequest<GetCustomersResponse>
    {
    }

    public class GetCustomersResponse : BaseResponse
    {
        public IEnumerable<Contracts.Customer> Customers { get; set; }
    }

    public class GetCustomersRequestHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
    {
        private readonly IFinancialContext _financialContext;

        public GetCustomersRequestHandler(IFinancialContext financialContext)
        {
            _financialContext = financialContext;
        }


        public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _financialContext.Customers.ToListAsync(cancellationToken);

            return new GetCustomersResponse
            {
                Customers = customers.Select(_=>new Contracts.Customer
                {
                    Id = _.Id,
                    Name = _.Name,
                    Surname = _.Surname
                })
            };
        }
    }
}
