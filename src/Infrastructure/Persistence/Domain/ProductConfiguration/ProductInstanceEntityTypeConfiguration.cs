using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Domain.ProductConfiguration
{
    public class ProductInstanceEntityTypeConfiguration : IEntityTypeConfiguration<ProductInstance>
    {
        public void Configure(EntityTypeBuilder<ProductInstance> builder)
        {
            builder.HasMany(x => x.IssuanceDocuments)
                   .WithMany(x => x.ProductInstances)
                   .UsingEntity(x => x.ToTable("IssuanceDocumentProductInstance"));

            builder.Property(x => x.SerialNumber).HasColumnType("varchar(100)");

        }
    }
}
