namespace InventoryManagement.Domain.Product
{
    public class Product
    {
        public long Id { get; set; }
        public string BrandName { get; set; }
        public ProductType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductInstance> ProductInstances { get; set; }


        public static Product Create(string brandName, ProductType productType)
        {
            return new Product { BrandName = brandName, Type = productType, CreatedAt = DateTime.Now };
        }

        public void AddProductInstances(IEnumerable<ProductInstance> productInstances)
        {
            ProductInstances ??= [];
            ProductInstances.AddRange(productInstances);
        }
    }
}
