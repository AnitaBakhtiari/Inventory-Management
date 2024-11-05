using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Domain.ProductConfiguration
{
    public class ProductRepository(InventoryManagementDbContext dbContext) : IProductRepository
    {
        private readonly InventoryManagementDbContext _dbContext = dbContext;

        public async Task AddAsync(Product product)
        {
            await _dbContext.AddAsync(product);
        }


        public Task<Product?> GetByBrandNameAndProductTypeAsync(string brandName, ProductType productType)
        {
            return _dbContext.Products
                .FirstOrDefaultAsync(x => x.BrandName.ToLower() == brandName.ToLower() && x.Type == productType);
        }

        public async Task<Product?> GetByIdAsync(long productId)
        {
            return await _dbContext.Products
                                   .Include(x => x.ProductInstances)
                                   .FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
