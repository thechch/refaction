using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Model;
using Xunit;

namespace Xero.RefactorMe.Data.Tests.UnitTests {

    public class ProductOptionRepositoryTests {

        /// <summary>
        /// Sets up a fixture class to use across all the tests
        /// </summary>
        public class ProductOptionRepositoryFixture : IDisposable {

            public readonly IProductOptionRepository _productOptionRepository;

            public ProductOptionRepositoryFixture () {
                var testProductOptions = new List<ProductOption> {
                    new ProductOption { Id = new Guid("5c2996ab-54ad-4999-92d2-89245682d534"), Name = "Test option 1" },
                    new ProductOption { Id = new Guid("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03"), Name = "Test option 2" },
                }.AsQueryable ();

                var mockSet = new Mock<DbSet<ProductOption>> ();
                mockSet.As<IQueryable<ProductOption>> ().Setup (m => m.Provider).Returns (testProductOptions.Provider);
                mockSet.As<IQueryable<ProductOption>> ().Setup (m => m.Expression).Returns (testProductOptions.Expression);
                mockSet.As<IQueryable<ProductOption>> ().Setup (m => m.ElementType).Returns (testProductOptions.ElementType);
                mockSet.As<IQueryable<ProductOption>> ().Setup (m => m.GetEnumerator ()).Returns (testProductOptions.GetEnumerator ());

                var mockContext = new Mock<RefactorMeDbContext> ();
                mockContext.Setup (c => c.ProductOptions).Returns (mockSet.Object);

                _productOptionRepository = new ProductOptionRepository(mockContext.Object);
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
		[CollectionDefinition("ProductOptionRepository")]
		public class ProductOptionRepositoryCollection : ICollectionFixture<ProductOptionRepositoryFixture>
        { 

        }

        /// <summary>
        /// Tests for GetSingle(Guid id)
        /// </summary>
        [Collection("ProductOptionRepository")]
        public class GetSingleMethodShould
        {
            private readonly ProductOptionRepositoryFixture _fixture;

			public GetSingleMethodShould(ProductOptionRepositoryFixture fixture)
			{
				_fixture = fixture;
			}

            [Theory]
            [InlineData("5c2996ab-54ad-4999-92d2-89245682d534")]
            [InlineData("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03")]
            public void ReturnAListOfProducts(string id)
            {
                var optionId = new Guid(id);
                var productOption = _fixture._productOptionRepository.GetSingle(optionId);

                Assert.NotNull(productOption);
                Assert.IsType<ProductOption>(productOption);
            }

            [Fact]
            public void ReturnNullWhenProductOptionHasNoSuchId(string name)
            {
                var guid = new Guid();
                var productOption = _fixture._productOptionRepository.GetSingle(guid);

                Assert.Null(productOption);
            }
        }
    }
}