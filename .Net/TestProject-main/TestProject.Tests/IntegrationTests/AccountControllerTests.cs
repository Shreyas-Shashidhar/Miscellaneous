using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestProject.Tests.Extensions;
using TestProject.Tests.Helpers;
using TestProject.WebAPI;
using TestProject.WebAPI.Application.Accounts;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task AccountsController_GetAllAccounts_ReturnsValidData()
        {
            //Act
            var response = await _client.GetAsync("/api/accounts");
            var result = await response.ReadBody<ApiResponse<IEnumerable<Account>>>();

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task AccountsController_CreateAccountWithValidUserId_ReturnsValidData()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var userContent = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 5000,
                MontlySalary = 7000,
            }.ToJsonContent();



            //Act
            var userCreateResponse = await _client.PostAsync("/api/users", userContent);
            var userCreateResult = await userCreateResponse.ReadBody<ApiResponse<User>>();

            var accountContent = new Account()
            {
                UserId = userCreateResult.Data.Id
            }.ToJsonContent();
            var accountCreateResponse = await _client.PostAsync("/api/accounts", accountContent);
            var accountCreateResult = await accountCreateResponse.ReadBody<ApiResponse<AccountDto>>();

            //Assert
            accountCreateResponse.EnsureSuccessStatusCode();
            accountCreateResult.Should().NotBeNull();
            accountCreateResult.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task AccountsController_CreateAccountWithInvalidUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var dummyUserId = $"Lorem_{RandomHelper.GetNumber()}";
            var accountContent = new Account()
            {
                UserId = dummyUserId
            }.ToJsonContent();

            //Act

            var accountCreateResponse = await _client.PostAsync("/api/accounts", accountContent);
            var accountCreateResult = await accountCreateResponse.ReadBody<ApiResponse<AccountDto>>();

            //Assert
            accountCreateResponse.IsSuccessStatusCode.Should().BeFalse();
            accountCreateResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            accountCreateResult.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async Task AccountsController_GetAccountWithValidUserId_ReturnsValidData()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var userContent = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 5000,
                MontlySalary = 7000,
            }.ToJsonContent();



            //Act
            var userCreateResponse = await _client.PostAsync("/api/users", userContent);
            var userCreateResult = await userCreateResponse.ReadBody<ApiResponse<User>>();

            var accountContent = new Account()
            {
                UserId = userCreateResult.Data.Id
            }.ToJsonContent();

            await _client.PostAsync("/api/accounts", accountContent);

            var accountGetResponse = await _client.GetAsync($"/api/accounts/{userCreateResult.Data.Id}");
            var accountGetResult = await accountGetResponse.ReadBody<ApiResponse<IEnumerable<AccountDto>>>();

            //Assert
            accountGetResponse.EnsureSuccessStatusCode();
            accountGetResult.Should().NotBeNull();
            accountGetResult.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task AccountsController_GetAccountWithInvalidUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var dummyUserId = $"Lorem_{RandomHelper.GetNumber()}";

            //Act

            var accountGetResponse = await _client.GetAsync($"/api/accounts/{dummyUserId}");
            var accountGetResult = await accountGetResponse.ReadBody<ApiResponse<IEnumerable<AccountDto>>>();

            //Assert
            accountGetResponse.IsSuccessStatusCode.Should().BeFalse();
            accountGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            accountGetResult.ErrorObject.Should().NotBeNull();
        }
    }
}
