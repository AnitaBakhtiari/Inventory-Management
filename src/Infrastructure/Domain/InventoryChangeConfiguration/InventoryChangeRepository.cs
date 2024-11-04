using InventoryManagement.Domain.InventoryChanges;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Domain.InventoryChangeConfiguration
{
    public class InventoryChangeRepository : IInventoryChangeRepository
    {
        private readonly InventoryManagementDbContext _dbContext;

        public InventoryChangeRepository(InventoryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(InventoryChange inventoryChange)
        {
            await _dbContext.AddAsync(inventoryChange);
            
        }

        public Task<InventoryChange?> GetByIdAsync(Guid inventoryChangeId)
        {
            return _dbContext.InventoryChanges
                             .Include(ic => ic.ProductInstances)
                             .ThenInclude(pi => pi.Product)
                             .FirstOrDefaultAsync(ic => ic.Id == inventoryChangeId);
        }
    }
}
