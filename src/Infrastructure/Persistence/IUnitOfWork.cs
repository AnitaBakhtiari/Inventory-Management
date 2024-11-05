namespace InventoryManagement.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }



    public class UnitOfWork(InventoryManagementDbContext context) : IUnitOfWork
    {
        private readonly InventoryManagementDbContext _dbContext = context;

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}