using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Data.Repositories;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data
{
    public class ProductRepository : EntityRepository<Product>, IProductRepository
    {
        public ProductRepository(RefactorMeDbContext context)
            : base(context)
        { }
    }
}