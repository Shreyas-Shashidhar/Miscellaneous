using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;

namespace Battleship.Api.Application.Ships.Commands.CreateShip
{
    public class ShipViewModel : IMapFrom<Ship>
    {
        public int ShipId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ship, ShipViewModel>();
        }
    }
}
