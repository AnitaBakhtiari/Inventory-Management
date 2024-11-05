using InventoryManagement.Domain.Products;

namespace InventoryManagement.Models
{
    public record InventoryEntryRequest(string BrandName, List<string> SerialNumbers, ProductType ProductType = ProductType.Laptop);
}
