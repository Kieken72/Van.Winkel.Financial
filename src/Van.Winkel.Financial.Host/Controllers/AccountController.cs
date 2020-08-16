using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Van.Winkel.Financial.Contracts;
using Van.Winkel.Financial.Host.Dto;
using Van.Winkel.Financial.Service.Account;

namespace Van.Winkel.Financial.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<AccountController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("{id}")]
        public async Task<IActionResult> Post(Guid id, OpenAccount openAccount)
        {
            var request = new AddAccountRequest {CustomerId = id, InitialCredit = openAccount.InitialCredit};
            var response = await _mediator.Send(request);
            return response.ToActionResult(_=>_.Account);
        }
    }
}