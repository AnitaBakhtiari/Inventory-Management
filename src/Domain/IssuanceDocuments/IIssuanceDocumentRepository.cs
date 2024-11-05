namespace InventoryManagement.Domain.IssuanceDocuments
{
    public interface IIssuanceDocumentRepository
    {
        Task AddAsync(IssuanceDocument IssuanceDocument);
        Task<IssuanceDocument?> GetByIdAsync(Guid IssuanceDocumentId);
    }
}
