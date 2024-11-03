namespace Inventory_Management.Domain.Product.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product?> GetByBrandNameAndProductTypeAsync(string brandName, ProductType productType);
    }
}
