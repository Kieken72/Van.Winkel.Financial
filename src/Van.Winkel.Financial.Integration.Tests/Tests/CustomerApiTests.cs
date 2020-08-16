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

    public class CustomerApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly JsonSerializerOptions _options;

        public CustomerApiTests(CustomWebApplicationFactory<Startup> factory)
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
        public async Task Get_CustomerWithCorrectId_ReturnsCustomer()
        {
            
            // Act
            var response = await _client.GetAsync($"/api/customer/{Utilities.CustomerId}");
            var content = await response.Content.ReadAsStringAsync();

            var customer = JsonSerializer.Deserialize<Customer>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(Utilities.CustomerId, customer.Id);
        }

        [Fact]
        public async Task Get_CustomerWithWrongId_ReturnsNotFound()
        {

            // Act
            var response = await _client.GetAsync($"/api/customer/{Guid.Empty}");
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(ValidationErrorCode.CustomerNotFound, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_CustomerWithFields_ReturnsCustomer()
        {
            //Arrange

            var newCustomer = new Customer{Name = "Jakob",Surname = "Gates"};

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var customer = JsonSerializer.Deserialize<Customer>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(newCustomer.Surname, customer.Surname);
        }

        [Fact]
        public async Task Post_CustomerWithIncorrectNameLessThen1_ReturnsError()
        {
            //Arrange
            var newCustomer = new Customer { Name = "", Surname = "Gates" };

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ValidationErrorCode.InvalidMinNameLength, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_CustomerWithIncorrectNameMoreThen250_ReturnsError()
        {
            //Arrange
            var newCustomer = new Customer { Name = new string('*',251), Surname = "Gates" };

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ValidationErrorCode.InvalidMaxNameLength, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_CustomerWithIncorrectSurnameLessThen1_ReturnsError()
        {
            //Arrange
            var newCustomer = new Customer { Name = "Bill", Surname = "" };

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ValidationErrorCode.InvalidMinSurnameLength, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_CustomerWithIncorrectSurnameMoreThen250_ReturnsError()
        {
            //Arrange
            var newCustomer = new Customer { Name = "Bill", Surname = new string('*', 251) };

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ValidationErrorCode.InvalidMaxSurnameLength, bag.Errors.First().ValidationErrorCode);
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Post_CustomerWithIncorrectFields_ReturnsError()
        {
            //Arrange
            var newCustomer = new Customer { Name = "", Surname = new string('*', 251) };

            // Act
            var response = await _client.PostAsync($"/api/customer/", RequestHelper.CreateJson(newCustomer));
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True( bag.Errors.Any(_=>_.ValidationErrorCode == ValidationErrorCode.InvalidMaxSurnameLength));
            Assert.True(bag.Errors.Any(_ => _.ValidationErrorCode == ValidationErrorCode.InvalidMinNameLength));
            Assert.Equal(2, bag.Errors.Count());
        }

        [Fact]
        public async Task Put_CustomerWithCorrectData_ReturnsCustomer()
        {
            //Arrange
            var existingCustomer = new Customer { Name = "Jef", Surname = "Langleven" };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = RequestHelper.CreateJson(existingCustomer),
                RequestUri = new Uri($"https://localhost/api/customer/{Utilities.CustomerId}")
            };

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var customer = JsonSerializer.Deserialize<Customer>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(existingCustomer.Surname, customer.Surname);
        }

        [Fact]
        public async Task Put_CustomerWithIncorrectFields_ReturnsError()
        {
            //Arrange
            var existingCustomer = new Customer { Name = "", Surname = "Langleven" };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = RequestHelper.CreateJson(existingCustomer),
                RequestUri = new Uri($"https://localhost/api/customer/{Utilities.CustomerId}")
            };

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.True(bag.Errors.Any(_ => _.ValidationErrorCode == ValidationErrorCode.InvalidMinNameLength));
            Assert.Equal(1, bag.Errors.Count());
        }

        [Fact]
        public async Task Put_CustomerWithIncorrectId_ReturnsNotFound()
        {
            //Arrange
            var existingCustomer = new Customer { Name = "Jef", Surname = "Langleven" };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = RequestHelper.CreateJson(existingCustomer),
                RequestUri = new Uri($"https://localhost/api/customer/{Guid.Empty}")
            };

            // Act
            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var bag = JsonSerializer.Deserialize<ValidatioBagResult>(content, _options);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.True(bag.Errors.Any(_ => _.ValidationErrorCode == ValidationErrorCode.CustomerNotFound));
            Assert.Equal(1, bag.Errors.Count());
        }
    }


}
