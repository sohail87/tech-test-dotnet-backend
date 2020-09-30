using Moonpig.PostOffice.Core;
using System.Linq;

namespace Moonpig.PostOffice.Data
{
    public class ProductRepository : IProductRepository
    {
        private IDbContext _dbContext;

        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetProductLeadTime(int productId)
        {
            var supplierId = GetProduct(productId).SupplierId;
            return GetSupplier(supplierId).LeadTime;
        }

        private Supplier GetSupplier(int supplierId)
        {
            return _dbContext.Suppliers.Single(x => x.SupplierId == supplierId);
        }
        private Product GetProduct(int productId)
        {
            return _dbContext.Products.Single(x => x.ProductId == productId);
        }
    }
}