using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Domain.ProductConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.BrandName).HasMaxLength(100);

            builder.Ignore(x => x.GetAvailableProductInstance);

        }
    }
}
