
using Battleship.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship.Api.Application.Interfaces
{
    public interface IBattleshipDbContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Board> Boards { get; set; }
        DbSet<Ship> Ships { get; set; }
        DbSet<ShipPart> ShipParts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
