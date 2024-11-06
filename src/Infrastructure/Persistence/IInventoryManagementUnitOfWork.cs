using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;

namespace InventoryManagement.Infrastructure.Persistence
{
    public interface IInventoryManagementUnitOfWork: IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IIssuanceDocumentRepository IssuanceDocumentRepository { get; }
    }
}
