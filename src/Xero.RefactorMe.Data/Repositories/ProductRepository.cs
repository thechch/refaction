using System.Collections.Generic;
using System.Linq;
using Xero.RefactorMe.Data.Abstract;
using Xero.RefactorMe.Data.Repositories;
using Xero.RefactorMe.Model;

namespace Xero.RefactorMe.Data
{
    public class ProductRepository : EntityRepository<Product>, IProductRepository
    {
        private readonly RefactorMeDbContext _context;
        public ProductRepository(RefactorMeDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetByName(string productName)
        {
            IQueryable<Product> productsSet = _context.Set<Product>();
            var resultList = productsSet.Where(p => p.Name.ToLower() == productName.ToLower());
            return resultList.ToList();
        }
    }
}