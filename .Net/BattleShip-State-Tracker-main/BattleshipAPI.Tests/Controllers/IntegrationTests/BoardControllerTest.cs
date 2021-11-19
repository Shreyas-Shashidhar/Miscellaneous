using Battleship.Api;
using Battleship.Api.Application.Boards.Commands.CreateBoard;
using Battleship.Api.Application.Interfaces;
using BattleshipAPI.Tests.Helper;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BattleshipAPI.Tests.Controllers.IntegrationTests
{
    public class BoardControllerTest : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly TestWebApplicationFactory<Startup> _factory;
        public BoardControllerTest(TestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateBoard_PostCommand_ReturnsNewBoardIdAndDimensions()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateBoardCommand
            {
                GameId = 1,
                DimensionX = 10,
                DimensionY = 10
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/boards", requestContent);
            var content = await JsonUtil.GetResponseContent<BoardViewModel>(response);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            content.BoardId.Should().BePositive();
            content.DimensionX.Should().BePositive();
            content.DimensionY.Should().BePositive();
        }

        [Fact]
        public async Task CreateBoard_PostCommandWithValidGameId_ReturnsBadRequestResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateBoardCommand
            {
                GameId = 1
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/boards", requestContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateBoard_PostCommandWithInvalidGameId_ReturnsNotFoundResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateBoardCommand
            {
                GameId = 10
            };
            var requestContent = JsonUtil.GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/boards", requestContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
