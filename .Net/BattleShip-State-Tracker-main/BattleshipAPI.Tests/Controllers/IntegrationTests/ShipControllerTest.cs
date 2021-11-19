using Battleship.Api;
using Battleship.Api.Application.Ships.Commands.AttackShip;
using Battleship.Api.Application.Ships.Commands.CreateShip;
using Battleship.Api.Domain;
using BattleshipAPI.Tests.Helper;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace BattleshipAPI.Tests.Controllers.IntegrationTests
{
    public class ShipControllerTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> _factory;

        public ShipControllerTest(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateShip_ReturnsNewShip()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateShipCommand
            {
                BoardId = 1,
                BowX = 3,
                BowY = 1,
                Length = 2,
                Orientation = ShipOrientation.Horizontal
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/ships", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await JsonUtil.GetResponseContent<ShipViewModel>(response);

            // Assert
            content.ShipId.Should().BePositive();
        }

        [Fact]
        public async Task AttackShip_ReturnsHit()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new AttackShipCommand
            {
                BoardId = 1,
                AttackX = 1,
                AttackY = 1
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await JsonUtil.GetResponseContent<AttackViewModel>(response);

            // Assert
            content.Hit.Should().BeTrue();
            content.X.Should().Be(command.AttackX);
            content.Y.Should().Be(command.AttackY);
        }

        [Fact]
        public async Task AttackWater_ReturnsMiss()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new AttackShipCommand
            {
                BoardId = 1,
                AttackX = 9,
                AttackY = 9
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PutAsync("/api/ships/attack", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await JsonUtil.GetResponseContent<AttackViewModel>(response);

            // Assert
            content.Hit.Should().BeFalse();
            content.X.Should().Be(command.AttackX);
            content.Y.Should().Be(command.AttackY);
        }

    }
}
