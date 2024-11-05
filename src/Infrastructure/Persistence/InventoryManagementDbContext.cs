﻿using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence
{
    public class InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInstance> ProductInstances { get; set; }
        public DbSet<InventoryChange> InventoryChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseHiLo();
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}