using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Data.Repositories;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data
{
    public class ProductOptionRepository : EntityRepository<ProductOption>, IProductOptionRepository
    {
        public ProductOptionRepository(RefactorMeDbContext context)
            : base(context)
        { }
    }
}