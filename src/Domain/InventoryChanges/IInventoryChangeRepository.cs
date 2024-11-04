
namespace InventoryManagement.Domain.InventoryChanges
{
    public interface IInventoryChangeRepository
    {
        Task AddAsync(InventoryChange inventoryChange);
        Task<InventoryChange?> GetByIdAsync(Guid inventoryChangeId);
    }
}
