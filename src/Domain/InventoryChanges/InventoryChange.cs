using InventoryManagement.Domain.Products;

namespace InventoryManagement.Domain.InventoryChanges
{
    public class InventoryChange
    {
        public Guid Id { get; set; }
        public InventoryChangeType Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<ProductInstance> ProductInstances { get; set; } 

        public static InventoryChange CreateEntry(IEnumerable<ProductInstance> ProductInstances)
        {
            return new InventoryChange()
            {
                Type = InventoryChangeType.Entry,
                ProductInstances = ProductInstances,
                CreatedOn = DateTime.Now
            };
        }

        public static InventoryChange CreateExit(IEnumerable<ProductInstance> ProductInstances)
        {
            return new InventoryChange()
            {
                Type = InventoryChangeType.Exit,
                ProductInstances = ProductInstances,
                CreatedOn = DateTime.Now
            };
        }

    }
}
