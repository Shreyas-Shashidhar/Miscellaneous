using Battleship.Api.Application.Ships.Commands.AttackShip;
using Battleship.Api.Application.Ships.Commands.CreateShip;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Battleship.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Ship to add to an existing Board.
        /// </summary>
        /// <param name="command">Specify the Board and details of the new Ship: Bow X, Y, Length(2-4) and Orientation(0 - Horizontal, 1 - Vertical).</param>
        /// <returns>The new ShipId</returns>
        /// <response code="201">Returns a json object containing the id for the new Ship.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The game Board could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost]
        [Produces(typeof(ShipViewModel))]
        [ProducesResponseType(typeof(ShipViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ShipViewModel>> Create([FromBody] CreateShipCommand command)
        {
            var ship = await _mediator.Send(command);

            return Created(string.Empty, ship);
        }

        /// <summary>
        /// Attack a Ship on an existing Board.
        /// </summary>
        /// <param name="command">Specify the Board and X, Y values for your attack.</param>
        /// <returns>Hit (with X, Y) or Miss.</returns>
        /// <response code="200">Returns a json object containing the results of the Attack.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="404">The game Board could not be found.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPut("attack")]
        [Produces(typeof(ShipViewModel))]
        [ProducesResponseType(typeof(AttackViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AttackViewModel>> Attack([FromBody] AttackShipCommand command)
        {
            var attack = await _mediator.Send(command);

            return Ok(attack);
        }
    }
}
