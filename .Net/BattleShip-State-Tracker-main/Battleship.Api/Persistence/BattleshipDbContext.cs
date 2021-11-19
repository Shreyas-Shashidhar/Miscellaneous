using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Api.Persistence
{
    public class BattleshipDbContext : DbContext, IBattleshipDbContext
    {
        public BattleshipDbContext(DbContextOptions<BattleshipDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipPart> ShipParts { get; set; }
    }
}
