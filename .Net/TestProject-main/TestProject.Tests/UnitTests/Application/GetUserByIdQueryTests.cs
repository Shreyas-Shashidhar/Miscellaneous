using FluentAssertions;
using Moq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Tests.Helpers;
using TestProject.WebAPI.Application.Users.Queries;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.UnitTests.Application
{
    public class GetUserByIdQueryTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private GetUserByIdQueryHandler _queryHandler;

        public GetUserByIdQueryTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _queryHandler = new GetUserByIdQueryHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async void GetUserById_OnValidUserId_ReturnsSuccess()
        {
            // Arrange
            var dummyId = $"Lorem_{RandomHelper.GetNumber()}";
            var query = new GetUserByIdQuery()
            {
                UserId = dummyId
            };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).ReturnsAsync(new User());

            //Act

            var result = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ErrorObject.Should().BeNull();

        }

        [Fact]
        public async void GetUserById_OnInValidUserId_ReturnsSuccess()
        {
            // Arrange
            var query = new GetUserByIdQuery()
            {
                UserId = null,
            };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(null)).Returns(Task.FromResult<User>(null));

            //Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(null), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.ErrorObject.Should().NotBeNull();
        }
    }
}
