using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Xero.RefactorMe.Web.Tests.IntegrationTests
{
    public class ProductOptionsControllerTests
    {
        /// <summary>
        /// Arrange. Sets up test runner.
        /// </summary>
        public class ProductOptionsControllerFixture : IDisposable
        {
            private readonly TestServer _server;
            private readonly HttpClient _client;

            public ProductOptionsControllerFixture()
            {
                _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
                _client = _server.CreateClient();
            }

            public async Task<string> GetMethodResponseString(string endpoint, string querystring = "")
            {
                var request = endpoint;
                if (!string.IsNullOrEmpty(querystring))
                {
                    request += "?" + querystring;
                }
                var response = await _client.GetAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }

            public void Dispose()
            {

            }
        }

        /// <summary>
        /// This class has no code, and is never created. Its purpose is simply
        /// to be the place to apply [CollectionDefinition] and all the
        /// ICollectionFixture<> interfaces.
        /// </summary>
		[CollectionDefinition("ProductOptionsController")]
		public class ProductOptionsControllerCollection : ICollectionFixture<ProductOptionsControllerFixture>
        { 

        }

        [Collection("ProductOptionsController")]
        public class ProductOptionsControllerGetOptionShould
        {
            private readonly ProductOptionsControllerFixture _fixture;

            public ProductOptionsControllerGetOptionShould(ProductOptionsControllerFixture fixture)
            {
                _fixture = fixture;
            }

            /// <summary>
            /// Test if endpoint returns a not empty result
            /// </summary>
            [Fact]
            public async Task ReturnEmptyResultWhenNoGuidIdProvided()
            {
                // Act
                var querystring = "/products/123/options/345/";
                var responseString = await _fixture.GetMethodResponseString(querystring);

                // Assert
                Assert.Contains("description", responseString.ToLower());
            }
        }
    }
}
