using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Xero.RefactorMe.Web.Tests.IntegrationTests
{
    public class ProductsControllerTests
    {
        /// <summary>
        /// Arrange. Sets up test runner.
        /// </summary>
        public class ProductControllerFixture : IDisposable
        {
            private readonly TestServer _server;
            private readonly HttpClient _client;

            public ProductControllerFixture()
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
		[CollectionDefinition("ProductController")]
		public class ProductOptionsCollection : ICollectionFixture<ProductControllerFixture>
        { 

        }

        [Collection("ProductController")]
        public class ProductsControllerSearchByNameShould
        {
            private readonly ProductControllerFixture _fixture;

            public ProductsControllerSearchByNameShould(ProductControllerFixture fixture)
            {
                _fixture = fixture;
            }

            /// <summary>
            /// Test if endpoint returns a not empty result
            /// </summary>
            [Fact]
            public async Task ReturnNotEmptyResult()
            {
                // Act
                var responseString = await _fixture.GetMethodResponseString("/products", "/Apple%20iPhone%206S");

                // Assert
                Assert.Contains("description", responseString.ToLower());
            }
        }
    }
}
