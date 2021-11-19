using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestProject.WebAPI.Application.Users.Commands;
using TestProject.WebAPI.Application.Users.Queries;
using TestProject.WebAPI.Domain.Models;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetUsersListQuery());
            if (result == null)
                return NotFound();
            else
                return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery() { UserId = userId });
            if (result == null)
                return BadRequest();
            else
                return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> CreateUser(User user)
        {
            var result = await _mediator.Send(new CreateUserCommand() { User = user });
            if (result == null)
                return BadRequest();
            else
                return StatusCode((int)result.StatusCode, result);
        }

    }
}
