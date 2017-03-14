using System.Collections.Generic;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> GetByName(string productName);
    }
    
    public interface IProductOptionRepository : IEntityRepository<ProductOption> { }
}