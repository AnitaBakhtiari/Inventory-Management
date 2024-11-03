using Inventory_Management.Domain.Product;
using Inventory_Management.Domain.Product.Products;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Infrastructure.Domain.ProductConfiguration
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

    }
}
