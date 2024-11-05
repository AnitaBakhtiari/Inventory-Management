using InventoryManagement.Application.Exceptions;
using System.Net;

namespace InventoryManagement.Domain.Products
{
    public class Product
    {
        public long Id { get; set; }
        public required string BrandName { get; set; }
        public ProductType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductInstance> ProductInstances { get; set; } = [];


        public static Product Create(string brandName, ProductType productType)
        {
            return new Product { BrandName = brandName, Type = productType, CreatedAt = DateTime.Now };
        }

        public void AddProductInstances(IEnumerable<ProductInstance> productInstances)
        {
            ProductInstances.AddRange(productInstances);
        }

        public List<ProductInstance> GetAvailableProductInstance => ProductInstances.Where(x => x.IsAvailable).ToList();

        public virtual bool HasInventory(int quantity) => GetAvailableProductInstance.Count >= quantity;

        public virtual List<ProductInstance> ReduceProductInstancesInventory(int quantity)
        {

            if (!HasInventory(quantity))
            {
                throw new BusinessException(ExceptionMessages.OutOfInventory, (int)HttpStatusCode.PreconditionFailed);
            }

            var availableProductInstances = GetAvailableProductInstance.Take(quantity).ToList();

            availableProductInstances.ForEach(instance => instance.IsAvailable = false);

            return availableProductInstances;

        }

        public void IncreaseProductInstanceInventory(IEnumerable<string> serialNumbers)
        {
            var selectedProductInstances = ProductInstances.Where(instance => serialNumbers.Contains(instance.SerialNumber));

            foreach (var productInstance in selectedProductInstances)
            {
                productInstance.IsAvailable = true;
            }
        }
    }

}

