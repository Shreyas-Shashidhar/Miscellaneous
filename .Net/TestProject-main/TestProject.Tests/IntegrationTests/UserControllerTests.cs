using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestProject.Tests.Extensions;
using TestProject.Tests.Helpers;
using TestProject.WebAPI;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Domain.Models;
using Xunit;

namespace TestProject.Tests.IntegrationTests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        public UserControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task UserController_GetAllUsers_ReturnsValidData()
        {
            // Arrange

            //Act
            var response = await _client.GetAsync("/api/users");
            var result = await response.ReadBody<ApiResponse<IEnumerable<User>>>();

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task UserController_CreateNewUser_ReturnsSuccessFulResult()
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
            var response = await _client.PostAsync("/api/users", userContent);
            var result = await response.ReadBody<ApiResponse<User>>();

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task UserController_CreateNewUserWithSameMailId_ReturnsFailedResult()
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
            // Create same user twice
            await _client.PostAsync("/api/users", userContent);
            var response = await _client.PostAsync("/api/users", userContent);
            var result = await response.ReadBody<ApiResponse<User>>();

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async Task UserController_CreateNewUserWithLessMonthlySalary_ReturnsFailedResult()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var userContent = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 500,
                MontlySalary = 700,
            }.ToJsonContent();

            //Act
            var response = await _client.PostAsync("/api/users", userContent);
            var result = await response.ReadBody<ApiResponse<User>>();

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.ErrorObject.Should().NotBeNull();
        }

        [Fact]
        public async Task UserController_CreateNewUserWithNegativeSalary_ReturnsBadRequestResult()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var userContent = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = 500,
                MontlySalary = -700,
            }.ToJsonContent();



            //Act
            var response = await _client.PostAsync("/api/users", userContent);

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UserController_CreateNewUserWithNegativeExpenses_ReturnsBadRequestResult()
        {
            // Arrange
            var dummyName = $"Lorem_{RandomHelper.GetNumber()}";
            var userContent = new User()
            {
                Name = dummyName,
                EmailAddress = $"{dummyName}@test.com",
                MontlyExpenses = -500,
                MontlySalary = 2700,
            }.ToJsonContent();



            //Act
            var response = await _client.PostAsync("/api/users", userContent);

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UserController_GetUserDetails_ReturnsSuccessFulResult()
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
            var createResponse = await _client.PostAsync("/api/users", userContent);
            var createResult = await createResponse.ReadBody<ApiResponse<User>>();

            var response = await _client.GetAsync($"/api/users/{createResult.Data.Id}");
            var result = await response.ReadBody<ApiResponse<User>>();

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().Be(createResult.Data.Id);
        }

        [Fact]
        public async Task UserController_GetUserDetails_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var dummyID = $"dummyId_{RandomHelper.GetNumber()}";

            //Act
            var response = await _client.GetAsync($"/api/users/{dummyID}");

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
