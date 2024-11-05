namespace InventoryManagement.Domain.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product?> GetByBrandNameAndProductTypeAsync(string brandName, ProductType productType);
        Task<Product?> GetByIdAsync(long productId);
        Task<(Product?, ProductInstance)> GetBySerialNumberAsync(string serialNumber);
    }
}
