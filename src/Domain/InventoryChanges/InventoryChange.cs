﻿using InventoryManagement.Domain.Products;

namespace InventoryManagement.Domain.InventoryChanges
{
    public class InventoryChange
    {
        public Guid Id { get; set; }
        public InventoryChangeType Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<ProductInstance> ProductInstances { get; set; } = [];

        public static InventoryChange Create(InventoryChangeType Type, IEnumerable<ProductInstance> ProductInstances)
        {
            return new InventoryChange()
            {
                Type = Type,
                ProductInstances = ProductInstances
            };
        }

    }
}
