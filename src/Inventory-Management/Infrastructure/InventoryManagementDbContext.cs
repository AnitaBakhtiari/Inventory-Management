using Inventory_Management.Domain.InventoryChanges;
using Inventory_Management.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Infrastructure
{
    public class InventoryManagementDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInstance> ProductInstances { get; set; }
        public DbSet<InventoryChange> InventoryChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
