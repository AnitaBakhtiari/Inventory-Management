using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence
{
    public class InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInstance> ProductInstances { get; set; }
        public DbSet<IssuanceDocument> IssuanceDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
