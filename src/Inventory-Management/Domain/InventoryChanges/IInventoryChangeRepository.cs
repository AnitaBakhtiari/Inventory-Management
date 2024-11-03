namespace Inventory_Management.Domain.InventoryChanges
{
    public interface IInventoryChangeRepository
    {
        Task AddAsync(InventoryChange inventoryChange);
    }
}
