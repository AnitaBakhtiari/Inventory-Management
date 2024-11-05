namespace InventoryManagement.Models
{
    public record InventoryDetailsResponse
    (
        Guid InventoryChangeId,
        InventoryDetailsItem[] Items
    );

    public record InventoryDetailsItem
    (
        long ProductId,
        int Quantity,
        string ProductBrandName
    );

}
