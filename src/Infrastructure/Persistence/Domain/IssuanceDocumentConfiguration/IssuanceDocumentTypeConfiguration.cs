using InventoryManagement.Domain.IssuanceDocuments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Domain.IssuanceDocumentConfiguration
{
    public class IssuanceDocumentTypeConfiguration : IEntityTypeConfiguration<IssuanceDocument>
    {
        public void Configure(EntityTypeBuilder<IssuanceDocument> builder)
        {
            builder
           .Property(e => e.Id)
           .HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}
