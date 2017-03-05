using Xero.RefactorMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xero.RefactorMe.Data
{
    public class RefactorMeDbInitializer
    {
        private static RefactorMeDbContext context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (RefactorMeDbContext)serviceProvider.GetService(typeof(RefactorMeDbContext));

            Initialize();
        }

        private static void Initialize()
        {
            if (!context.Products.Any())
            {
                Product product1 = new Product()
                {
                    Id = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                    Name = "Samsung Galaxy S7",
                    Description = "Newest mobile product from Samsung.",
                    Price = new Decimal(1024.99),
                    DeliveryPrice = new Decimal(16.99),
                    ProductOptions = new List<ProductOption>
                    {
                        new ProductOption()
                        {
                                Id = new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"),
                                ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                                Name = "White",
                                Description = "White Samsung Galaxy S7"
                        },
                        new ProductOption()
                        {
                                Id = new Guid("a21d5777-a655-4020-b431-624bb331e9a2"),
                                ProductId = new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),
                                Name = "Black",
                                Description = "Black Samsung Galaxy S7"
                        },

                    }
                };
                Product product2 = new Product()
                {
                    Id = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                    Name = "Apple iPhone 6S",
                    Description = "Newest mobile product from Apple.",
                    Price = new Decimal(1299.99),
                    DeliveryPrice = new Decimal(15.99)
                    ProductOptions = new List<ProductOption>
                    {
                        new ProductOption()
                        {
                                Id = new Guid("5c2996ab-54ad-4999-92d2-89245682d534"),
                                ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                                Name = "Rose Gold",
                                Description = "Gold Apple iPhone 6S"
                        },
                        new ProductOption()
                        {
                                Id = new Guid("9ae6f477-a010-4ec9-b6a8-92a85d6c5f03"),
                                ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                                Name = "White",
                                Description = "White Apple iPhone 6S"
                        },
                        new ProductOption()
                        {
                                Id = new Guid("4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef"),
                                ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),
                                Name = "Black",
                                Description = "Black Apple iPhone 6S"
                        },
                    }
                };

                context.Products.AddRange(product1, product2);
                context.SaveChanges();
            }
        }
    }
}