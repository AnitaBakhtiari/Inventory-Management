using InventoryManagement.Domain.IssuanceDocuments;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Domain.IssuanceDocumentConfiguration
{
    public class IssuanceDocumentRepository : IIssuanceDocumentRepository
    {
        private readonly InventoryManagementDbContext _dbContext;

        public IssuanceDocumentRepository(InventoryManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(IssuanceDocument IssuanceDocument)
        {
            await _dbContext.AddAsync(IssuanceDocument);

        }

        public Task<IssuanceDocument?> GetByIdAsync(Guid IssuanceDocumentId)
        {
            return _dbContext.IssuanceDocuments
                             .Include(ic => ic.ProductInstances)
                             .ThenInclude(pi => pi.Product)
                             .FirstOrDefaultAsync(ic => ic.Id == IssuanceDocumentId);
        }
    }
}
