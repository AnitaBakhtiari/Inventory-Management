using Inventory_Management.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory_Management.Infrastructure.Domain.ProductConfiguration
{
    public class ProductInstanceEntityTypeConfiguration : IEntityTypeConfiguration<ProductInstance>
    {
        public void Configure(EntityTypeBuilder<ProductInstance> builder)
        {
            builder.HasMany(x => x.InventoryChanges)
                   .WithMany(x => x.ProductInstances)
                   .UsingEntity(x => x.ToTable("InventoryChangeProductInstance"));
        }
    }
}
