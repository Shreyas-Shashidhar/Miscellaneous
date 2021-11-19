using MediatR;

namespace Battleship.Api.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<GameViewModel>
    {
    }
}
