using FluentAssertions;
using Moq;
using System.Net;
using System.Threading;
using TestProject.Tests.Helpers;
using TestProject.WebAPI.Application.Users.Commands;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.UnitTests.Application
{
    public class CreateUserCommandTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private CreateUserCommandHandler _commandHandler;

        public CreateUserCommandTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _commandHandler = new CreateUserCommandHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async void CreateUserCommand_ValidData_ReturnsSuccess()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var user = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 1500,
                MontlySalary = 2700,
            };
            var command = new CreateUserCommand()
            {
                User = user
            };
            _userRepositoryMock.Setup(ur => ur.CreateUserEntryAsync(user)).ReturnsAsync(user);

            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.CreateUserEntryAsync(user), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.ErrorObject.Should().BeNull();
        }

        [Fact]
        public async void CreateUserCommand_LessMonthlySalary_ReturnsFailureResult()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var user = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 500,
                MontlySalary = 700,
            };
            var command = new CreateUserCommand()
            {
                User = user
            };
            _userRepositoryMock.Setup(ur => ur.CreateUserEntryAsync(user)).ReturnsAsync(user);

            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.CreateUserEntryAsync(user), Times.Never);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async void CreateUserCommand_NegativeMonthlySalary_ReturnsSuccess()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var user = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 500,
                MontlySalary = -700,
            };
            var command = new CreateUserCommand()
            {
                User = user
            };
            _userRepositoryMock.Setup(ur => ur.CreateUserEntryAsync(user)).ReturnsAsync(user);

            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.CreateUserEntryAsync(user), Times.Never);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.ErrorObject.Should().NotBeNull();
        }
    }
}
