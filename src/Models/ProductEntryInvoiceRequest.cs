using InventoryManagement.Domain.Products;

namespace InventoryManagement.Models
{
    public record ProductEntryInvoiceRequest(string BrandName, ProductType ProductType, List<string> SerialNumbers);
}
