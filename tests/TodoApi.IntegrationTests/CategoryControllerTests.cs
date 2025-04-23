using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;

namespace TodoApi.IntegrationTests
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoryControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCategories_WithoutAuth_ReturnsUnauthorized()
        {
            var response = await _client.GetAsync("/api/category");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
