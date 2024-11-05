namespace InventoryManagement.Models
{
    public record IssuanceDocumentsResponse
    (
        Guid IssuanceDocumentId,
        IssuanceDocumentsItem[] Items
    );

    public record IssuanceDocumentsItem
    (
        long ProductId,
        int Quantity,
        string ProductBrandName
    );

}
