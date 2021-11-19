using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;

namespace Battleship.Api.Application.Games.Commands.CreateGame
{
    public class GameViewModel : IMapFrom<Game>
    {
        public int GameId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Game, GameViewModel>();
        }
    }
}
