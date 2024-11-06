using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;

namespace InventoryManagement.Infrastructure.Persistence
{
    public class InventoryManagementUnitOfWork : UnitOfWork, IInventoryManagementUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IIssuanceDocumentRepository IssuanceDocumentRepository { get; }

        public InventoryManagementUnitOfWork(InventoryManagementDbContext dbContext,
            IProductRepository productRepository,
            IIssuanceDocumentRepository issuanceDocumentRepository) : base(dbContext)
        {
            ProductRepository = productRepository;
            IssuanceDocumentRepository = issuanceDocumentRepository;
        }

    }
}
