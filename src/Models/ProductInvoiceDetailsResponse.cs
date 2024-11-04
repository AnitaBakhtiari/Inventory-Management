namespace InventoryManagement.Models
{
    public record ProductInvoiceDetailsResponse
    (
        Guid InventoryChangeId,
        ProductInvoiceItem[] ProductInvoiceItems
    );

    public record ProductInvoiceItem
    (
        long ProductId,
        int Quantity,
        string ProductBrandName
    );

}
