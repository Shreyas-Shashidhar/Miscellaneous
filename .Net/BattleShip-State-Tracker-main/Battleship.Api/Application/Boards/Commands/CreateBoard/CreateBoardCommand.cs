using MediatR;

namespace Battleship.Api.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<BoardViewModel>
    {
        public int GameId { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
    }
}
