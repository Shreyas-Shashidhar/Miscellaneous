using AutoMapper;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Tests.Helpers;
using TestProject.WebAPI.Application.Accounts;
using TestProject.WebAPI.Application.Accounts.Queries;
using TestProject.WebAPI.Domain.Interfaces;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.UnitTests.Application
{
    public class GetAccountListsByUserIdQueryTests
    {
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private GetAccountListsByUserIdQueryHandler _queryHandler;

        public GetAccountListsByUserIdQueryTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _mapperMock = new Mock<IMapper>();
            _queryHandler = new GetAccountListsByUserIdQueryHandler(_accountRepositoryMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void GetAccountByUserId_ValidData_ReturnsSuccess()
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

            var query = new GetAccountListsByUserIdQuery()
            {
                UserId = dummyId
            };

            var accounts = new List<Account>()
            {
                new Account() { UserId = dummyId, Id = $"Random_{RandomHelper.GetNumber()}" },
                new Account() { UserId = dummyId, Id = $"Random_{RandomHelper.GetNumber()}" },
                new Account() { UserId = dummyId, Id = $"Random_{RandomHelper.GetNumber()}" },
            };
            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).ReturnsAsync(user);
            _accountRepositoryMock.Setup(ar => ar.GetAllAccountsByUserIdAsync(dummyId)).ReturnsAsync(accounts);
            _mapperMock.Setup(mapper => mapper.Map<AccountDto>(It.IsAny<Account>())).Returns(new AccountDto() { Id = dummyId, UserId = dummyId });

            //Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.GetAllAccountsByUserIdAsync(dummyId), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ErrorObject.Should().BeNull();
        }

        [Fact]
        public async void GetAccountByUserId_InValidAccountData_ReturnsSuccess()
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

            var query = new GetAccountListsByUserIdQuery()
            {
                UserId = dummyId
            };

            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).ReturnsAsync(user);
            _accountRepositoryMock.Setup(ar => ar.GetAllAccountsByUserIdAsync(dummyId)).Returns(Task.FromResult<IEnumerable<Account>>(null));

            //Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.GetAllAccountsByUserIdAsync(dummyId), Times.Once);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async void GetAccountByUserId_InValidUserData_ReturnsSuccess()
        {
            // Arrange
            var dummyId = $"Lorem_{RandomHelper.GetNumber()}";

            var query = new GetAccountListsByUserIdQuery()
            {
                UserId = dummyId
            };

            _userRepositoryMock.Setup(ur => ur.GetUserByIdAsync(dummyId)).Returns(Task.FromResult<User>(null));

            //Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            //Assert
            _userRepositoryMock.Verify(ur => ur.GetUserByIdAsync(dummyId), Times.Once);
            _accountRepositoryMock.Verify(ur => ur.GetAllAccountsByUserIdAsync(It.IsAny<string>()), Times.Never);
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.ErrorObject.Should().NotBeNull();
        }
    }
}
