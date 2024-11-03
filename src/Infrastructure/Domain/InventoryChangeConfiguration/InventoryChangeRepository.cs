using InventoryManagement.Domain.InventoryChanges;

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
    }
}
