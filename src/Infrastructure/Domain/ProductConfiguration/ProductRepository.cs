using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Domain.ProductConfiguration
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagementDbContext _dbContext;

        public ProductRepository(InventoryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.AddAsync(product);
        }


        public Task<Product?> GetByBrandNameAndProductTypeAsync(string brandName, ProductType productType)
        {
            return _dbContext.Products.FirstOrDefaultAsync(x => x.BrandName == brandName && x.Type == productType);
        }

        public async Task<Product?> GetByIdAsync(long productId)
        {
            return await _dbContext.Products
                                   .Include(x => x.ProductInstances)
                                   .FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
