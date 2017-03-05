using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data.Abstract
{
    public interface IProductRepository: IEntityRepository<Product> {}

    public interface IProductOptionRepository: IEntityRepository<ProductOption> {}
}