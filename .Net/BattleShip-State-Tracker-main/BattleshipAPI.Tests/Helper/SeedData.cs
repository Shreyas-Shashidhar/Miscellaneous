using Battleship.Api.Application.Interfaces;
using Battleship.Api.Domain;
using Battleship.Api.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace BattleshipAPI.Tests.Helper
{
    public class SeedData
    {
        public static void Init(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var context = scopedServices.GetRequiredService<BattleshipDbContext>();
            context.Database.EnsureCreated();

            var game = new Game();
            context.Games.Add(game);

            var board = new Board
            {
                Game = game,
                DimensionX = 10,
                DimensionY = 10
            };
            context.Boards.Add(board);

            var ship = new Ship { Board = board };
            context.Ships.Add(ship);

            context.ShipParts.AddRange(new[]
            {
                        new ShipPart { Ship = ship, X = 1, Y = 1 },
                        new ShipPart { Ship = ship, X = 1, Y = 2 }
                    });

            context.SaveChanges();
        }
    }
}
