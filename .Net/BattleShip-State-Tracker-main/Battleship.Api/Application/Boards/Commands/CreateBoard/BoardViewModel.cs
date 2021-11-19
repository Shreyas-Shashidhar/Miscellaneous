using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;

namespace Battleship.Api.Application.Boards.Commands.CreateBoard
{
    public class BoardViewModel : IMapFrom<Board>
    {
        public int BoardId { get; set; }
        public int DimensionX { get; set; }
        public int DimensionY { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Board, BoardViewModel>();
        }
    }
}
