using System.Collections.Generic;

namespace Battleship.Api.Domain
{
    public class Game
    {
        public int GameId { get; set; }

        public ICollection<Board> Boards { get; private set; } = new HashSet<Board>();
    }
}
