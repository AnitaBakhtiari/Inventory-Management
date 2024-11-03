using Azure.Core;
using InventoryManagement.Domain.InventoryChanges;

namespace InventoryManagement.Domain.Product
{
    public class ProductInstance
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsAvailable { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<InventoryChange> InventoryChanges { get; set; }


        public static ProductInstance Create(string SerialNumber)
        {
            return new ProductInstance()
            {
                CreatedOn = DateTime.Now,
                IsAvailable = true,
                SerialNumber = SerialNumber
            };

        }
        public static IEnumerable<ProductInstance> Create(List<string> serialNumbers)
        {
            return serialNumbers.Select(ProductInstance.Create).ToList();

        }
    }
}
