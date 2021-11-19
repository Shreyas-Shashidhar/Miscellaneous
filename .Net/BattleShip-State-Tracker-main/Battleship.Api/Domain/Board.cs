using System.Collections.Generic;

namespace Battleship.Api.Domain
{
    public class Board
    {
        public int BoardId { get; set; }

        public int DimensionX { get; set; }

        public int DimensionY { get; set; }

        public Game Game { get; set; }

        public ICollection<Ship> Ships { get; private set; } = new HashSet<Ship>();
    }
}
