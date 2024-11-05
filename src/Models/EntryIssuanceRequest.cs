using InventoryManagement.Domain.Products;

namespace InventoryManagement.Models
{
    public record EntryIssuanceRequest(string BrandName, List<string> SerialNumbers, ProductType ProductType = ProductType.Laptop);
}
