namespace InventoryManagement.Domain.Products
{
    public interface IProductInstanceRepository
    {
        Task<ProductInstance?> GetBySerialNumberAsync(string serialNumber);
    }
}
