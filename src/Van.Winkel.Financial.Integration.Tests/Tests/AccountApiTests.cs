using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Van.Winkel.Financial.Contracts;
using Van.Winkel.Financial.Enums;
using Van.Winkel.Financial.Host;
using Van.Winkel.Financial.Integration.Tests.Helpers;
using Xunit;

namespace Van.Winkel.Financial.Integration.Tests.Tests
{

    public class AccountApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly JsonSerializerOptions _options;

        public AccountApiTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        
        [Fact]
        public async Task Post_WithValidArgumentsWithoutBalance_ReturnsAccount()
        {
            //Arrange
            var openAccount = new OpenAccount{ InitialCredit = 0 };

            // Act
            var response = await _client.PostAsync($"/api/account/{Utilities.CustomerId}", RequestHelper.CreateJson(openAccount));
            var content = await response.Content.ReadAsStringAsync();

            var account = JsonSerializer.Deserialize<Account>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(Utilities.CustomerId, account.CustomerId);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public async Task Post_WithValidArgumentsWithBalance_ReturnsAccount()
        {
            //Arrange
            var openAccount = new OpenAccount { InitialCredit = 5000 };

            // Act
            var response = await _client.PostAsync($"/api/account/{Utilities.CustomerId}", RequestHelper.CreateJson(openAccount));
            var content = await response.Content.ReadAsStringAsync();

            var account = JsonSerializer.Deserialize<Account>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(Utilities.CustomerId, account.CustomerId);
            Assert.Equal(5000, account.Balance);
        }

        [Fact]
        public async Task Post_WithBalanceLessThenZero_ReturnsError()
        {
            //Arrange
            var openAccount = new OpenAccount { InitialCredit = -1000 };

            // Act
            var response = await _client.PostAsync($"/api/account/{Utilities.CustomerId}", RequestHelper.CreateJson(openAccount));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ValidationErrorCode.InvalidUnderZeroInitialCredit, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_WithInvalidUser_ReturnsError()
        {
            //Arrange
            var openAccount = new OpenAccount { InitialCredit = -0 };

            // Act
            var response = await _client.PostAsync($"/api/account/{Guid.Empty}", RequestHelper.CreateJson(openAccount));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(ValidationErrorCode.CustomerNotFound, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }
    }
}