using Battleship.Api;
using Battleship.Api.Application.Games.Commands.CreateGame;
using BattleshipAPI.Tests.Helper;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BattleshipAPI.Tests.Controllers.IntegrationTests
{
    public class GameControllerTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> _factory;

        public GameControllerTest(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateGame_PostCommand_ReturnsNewGameId()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/games", null);
            var content = await JsonUtil.GetResponseContent<GameViewModel>(response);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            content.GameId.Should().BePositive();
        }
    }
}
