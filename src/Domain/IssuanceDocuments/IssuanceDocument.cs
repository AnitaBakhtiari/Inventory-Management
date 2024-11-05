using InventoryManagement.Domain.Products;

namespace InventoryManagement.Domain.IssuanceDocuments
{
    public class IssuanceDocument
    {
        public Guid Id { get; set; }
        public IssuanceDocumentType Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<ProductInstance> ProductInstances { get; set; }

        public static IssuanceDocument CreateEntry(IEnumerable<ProductInstance> ProductInstances)
        {
            return new IssuanceDocument()
            {
                Type = IssuanceDocumentType.Entry,
                ProductInstances = ProductInstances,
                CreatedOn = DateTime.Now
            };
        }

        public static IssuanceDocument CreateExit(IEnumerable<ProductInstance> ProductInstances)
        {
            return new IssuanceDocument()
            {
                Type = IssuanceDocumentType.Exit,
                ProductInstances = ProductInstances,
                CreatedOn = DateTime.Now
            };
        }

    }
}
