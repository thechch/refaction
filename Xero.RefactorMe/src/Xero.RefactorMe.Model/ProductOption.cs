using System;

namespace Xero.RefactorMe.Model
{
    public class ProductOption: IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }
        
        public Product Product { get; set; }
    }
}
