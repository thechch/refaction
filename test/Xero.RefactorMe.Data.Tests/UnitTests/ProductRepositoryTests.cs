using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Model;
using Xunit;

namespace Xero.RefactorMe.Data.Tests.UnitTests {
    public class ProductRepositoryTests {

        /// <summary>
        /// Sets up a fixture class to use across all the tests
        /// </summary>
        public class ProductRepositoryFixture : IDisposable {

            public readonly IProductRepository _productRepository;

            public ProductRepositoryFixture () {
                var testProducts = new List<Product> {
                    new Product { Name = "Test product 1" },
                    new Product { Name = "Test product 2" },
                }.AsQueryable ();

                var mockSet = new Mock<DbSet<Product>> ();
                mockSet.As<IQueryable<Product>> ().Setup (m => m.Provider).Returns (testProducts.Provider);
                mockSet.As<IQueryable<Product>> ().Setup (m => m.Expression).Returns (testProducts.Expression);
                mockSet.As<IQueryable<Product>> ().Setup (m => m.ElementType).Returns (testProducts.ElementType);
                mockSet.As<IQueryable<Product>> ().Setup (m => m.GetEnumerator ()).Returns (testProducts.GetEnumerator ());

                var mockContext = new Mock<RefactorMeDbContext> ();
                mockContext.Setup (c => c.Products).Returns (mockSet.Object);

                _productRepository = new ProductRepository (mockContext.Object);
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
		[CollectionDefinition("ProductRepository")]
		public class ProductRepositoryCollection : ICollectionFixture<ProductRepositoryFixture>
        { 

        }

        /// <summary>
        /// Tests for GetByName(string name)
        /// </summary>
        [Collection("ProductRepository")]
        public class GetByNameMethodShould
        {
            private readonly ProductRepositoryFixture _fixture;

			public GetByNameMethodShould(ProductRepositoryFixture fixture)
			{
				_fixture = fixture;
			}

            [Theory]
            [InlineData("Test product 1")]
            [InlineData("Test product 2")]
            public void ReturnAListOfProducts(string name)
            {
                var listOfProducts = _fixture._productRepository.GetByName(name);

                Assert.IsType<List<Product>>(listOfProducts);
            }
        }

        /// <summary>
        /// Tests for GetAll()
        /// </summary>
        [Collection("ProductRepository")]
        public class GetAllMethodShould
        {
            private readonly ProductRepositoryFixture _fixture;

			public GetAllMethodShould(ProductRepositoryFixture fixture)
			{
				_fixture = fixture;
			}

            [Fact]
            public void ReturnAListOfProducts(string name)
            {
                var allProducts = _fixture._productRepository.GetAll();

                Assert.IsNotType<IEnumerable<ProductOption>>(allProducts);
            }
        }
    }
}