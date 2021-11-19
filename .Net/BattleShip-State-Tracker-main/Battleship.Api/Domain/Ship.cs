using System.Collections.Generic;

namespace Battleship.Api.Domain
{
    public class Ship
    {

        public int ShipId { get; set; }

        public Board Board { get; set; }

        public ICollection<ShipPart> ShipParts { get; private set; } = new HashSet<ShipPart>();
    }
}
