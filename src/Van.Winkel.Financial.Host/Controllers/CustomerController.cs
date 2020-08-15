using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Van.Winkel.Financial.Contracts;
using Van.Winkel.Financial.Host.Dto;
using Van.Winkel.Financial.Service.Customer;

namespace Van.Winkel.Financial.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var request = new GetCustomerRequest {CustomerId = id};
            var response = await _mediator.Send(request);
            return response.ToActionResult(_=>_.Customer);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Get(Customer customer)
        {
            var request = new AddCustomerRequest() { Customer = customer };
            var response = await _mediator.Send(request);
            return response.ToActionResult(_ => _.Customer);
        }
    }
}