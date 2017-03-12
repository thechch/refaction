using System;
using System.Collections.Generic;

namespace Xero.RefactorMe.Model
{
    public class Product : IEntity
    {
        public Product()
        {
            ProductOptions = new List<ProductOption>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
