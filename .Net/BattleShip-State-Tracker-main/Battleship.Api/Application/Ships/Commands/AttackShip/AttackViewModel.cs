using AutoMapper;
using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;

namespace Battleship.Api.Application.Ships.Commands.AttackShip
{
    public class AttackViewModel : IMapFrom<ShipPart>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Hit { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShipPart, AttackViewModel>();
        }
    }
}
