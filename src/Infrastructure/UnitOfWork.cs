using InventoryManagement.Infrastructure.Persistence;

namespace InventoryManagement.Infrastructure
{
    public class UnitOfWork(InventoryManagementDbContext context) : IUnitOfWork
    {
        private readonly InventoryManagementDbContext _dbContext = context;

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}