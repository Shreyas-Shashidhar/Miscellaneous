using Battleship.Api.Application.Games.Commands.CreateGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Battleship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Game of Battleship.
        /// </summary>
        /// <returns>The new GameId.</returns>
        /// <response code="201">Returns a json object containing the id for the new Game.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(GameViewModel))]
        [ProducesResponseType(typeof(GameViewModel), 201)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GameViewModel>> Create()
        {
            var game = await _mediator.Send(new CreateGameCommand());

            return Created(string.Empty, game);
        }

    }
}
