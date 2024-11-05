using InventoryManagement.Domain.IssuanceDocuments;

namespace InventoryManagement.Domain.Products
{
    public class ProductInstance
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool IsAvailable { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<IssuanceDocument> IssuanceDocuments { get; set; } = [];


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
            return serialNumbers.Select(Create).ToList();

        }

        public void AddIssuanceDocuments(IssuanceDocument IssuanceDocument)
        {
            IssuanceDocuments.Add(IssuanceDocument);
        }
    }
}
