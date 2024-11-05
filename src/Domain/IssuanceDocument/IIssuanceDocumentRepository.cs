namespace InventoryManagement.Domain.InventoryChanges
{
    public interface IIssuanceDocumentRepository
    {
        Task AddAsync(IssuanceDocument IssuanceDocument);
        Task<IssuanceDocument?> GetByIdAsync(Guid IssuanceDocumentId);
    }
}
