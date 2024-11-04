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
            if (quantity <= 0)
            {
                throw new BusinessException(ExceptionMessages.QuantityGreaterThanZero, (int)HttpStatusCode.PreconditionFailed);
            }

            var availableProductInstances = ProductInstances.Where(x => x.IsAvailable).Take(quantity).ToList();

            if (availableProductInstances.Count == 0)
            {
                throw new BusinessException(ExceptionMessages.OutOfInventory, (int)HttpStatusCode.PreconditionFailed);
            }

            availableProductInstances.ForEach(instance => instance.IsAvailable = false);

            var effectedProductInstances = availableProductInstances.ToList();

            return effectedProductInstances;

        }

        public void IncreaseProductInstanceInventory(string serialNumber)
        {
            var productInstance = ProductInstances.SingleOrDefault(x => x.SerialNumber == serialNumber)
                ?? throw new BusinessException(ExceptionMessages.ProductNotFound, (int)HttpStatusCode.NotFound);

            productInstance.IsAvailable = true;
        }



    }
}
