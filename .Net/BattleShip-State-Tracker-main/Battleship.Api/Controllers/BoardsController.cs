using Battleship.Api.Application.Boards.Commands.CreateBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Battleship.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BoardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Board for an existing Game of Battleship.
        /// </summary>
        /// <param name="command">Create board command where the GameId should be specified and 
        /// Dimensions of X and Y  should be (5-50), if not specified will be initialzed to 10.
        /// </param>
        /// <returns>The new BoardId.</returns>
        /// <response code="201">Returns a json object containing the id of the new Board.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The Game could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(BoardViewModel))]
        [ProducesResponseType(typeof(BoardViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<BoardViewModel>> Create([FromBody] CreateBoardCommand command)
        {
            var board = await _mediator.Send(command);

            return Created(string.Empty, board);
        }
    }
}
