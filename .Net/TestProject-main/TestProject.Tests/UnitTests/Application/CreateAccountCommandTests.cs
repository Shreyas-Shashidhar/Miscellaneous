using AutoMapper;
using FluentAssertions;
using Moq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Tests.Helpers;
using TestProject.WebAPI.Application.Accounts;
using TestProject.WebAPI.Application.Accounts.Commands;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.UnitTests.Application
{

    public class CreateAccountCommandTests
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private CreateAccountCommandHandler _commandHandler;

        public CreateAccountCommandTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _mapperMock = new Mock<IMapper>();
            _commandHandler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void CreateAccountCommand_ValidData_ReturnsSuccess()
        {
            // Arrange
            var dummyId = $"Lorem_{RandomHelper.GetNumber()}";
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var user = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 1500,
                MontlySalary = 2700,
            };

            var command = new CreateAccountCommand()
            {
                UserId = dummyId
            };

            var account = new Account() { UserId = dummyId };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).ReturnsAsync(user);
            _accountRepositoryMock.Setup(ar => ar.CreateAccountEntryAsync(dummyId)).ReturnsAsync(account);
            _mapperMock.Setup(mapper => mapper.Map<AccountDto>(account)).Returns(new AccountDto() { Id = dummyId, UserId = dummyId });

            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.CreateAccountEntryAsync(dummyId), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            result.ErrorObject.Should().BeNull();
        }

        [Fact]
        public async void CreateAccountCommand_NoUser_ReturnsFailure()
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

            var dummyId = $"Lorem_{RandomHelper.GetNumber()}";

            var command = new CreateAccountCommand()
            {
                UserId = dummyId
            };

            var account = new Account() { UserId = dummyId };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).ReturnsAsync(user);
            _accountRepositoryMock.Setup(ur => ur.CreateAccountEntryAsync(dummyId)).Returns(Task.FromResult<Account>(null));


            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.CreateAccountEntryAsync(dummyId), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async void CreateAccountCommand_ValidUserEmptyAccount_ReturnsFailure()
        {
            // Arrange
            var dummyId = $"Lorem_{RandomHelper.GetNumber()}";

            var command = new CreateAccountCommand()
            {
                UserId = dummyId
            };

            var account = new Account() { UserId = dummyId };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).Returns(Task.FromResult<User>(null));


            //Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.CreateAccountEntryAsync(dummyId), Times.Never);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.ErrorObject.Should().NotBeNull();
        }
    }
}
