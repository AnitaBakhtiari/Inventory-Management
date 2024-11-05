using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Domain.ProductConfiguration
{
    public class ProductInstanceRepository(InventoryManagementDbContext dbContext) : IProductInstanceRepository
    {
        private readonly InventoryManagementDbContext _dbContext = dbContext;

        public Task<ProductInstance?> GetBySerialNumberAsync(string serialNumber)
        {
            return _dbContext.ProductInstances
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x => x.SerialNumber == serialNumber);
        }
    }
}
