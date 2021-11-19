using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestProject.WebAPI.Application.Accounts.Commands;
using TestProject.WebAPI.Application.Accounts.Queries;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await _mediator.Send(new GetAccountsListQuery());
            if (result == null)
                return NotFound();
            else
                return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAccountsByUserId(
            [FromRoute, Required] string userId)
        {
            var result = await _mediator.Send(new GetAccountListsByUserIdQuery() { UserId = userId });
            if (result == null)
                return BadRequest();
            else
                return StatusCode((int)result.StatusCode, result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody, Required] Account account)
        {
            var result = await _mediator.Send(new CreateAccountCommand() { UserId = account.UserId });
            if (result == null)
                return BadRequest();
            else
                return StatusCode((int)result.StatusCode, result);
        }
    }
}
