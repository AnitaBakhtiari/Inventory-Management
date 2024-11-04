using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class IssueProductEntryInvoiceCommandHandler : IRequestHandler<ProductEntryInvoiceCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;

        public IssueProductEntryInvoiceCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
        }

        public async Task<string> Handle(ProductEntryInvoiceCommand request, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetByBrandNameAndProductTypeAsync(request.BrandName, request.ProductType);

            if (product == null)
            {
                product = Product.Create(request.BrandName, request.ProductType);
                await _productRepository.AddAsync(product);
            }

            var productInstances = ProductInstance.Create(request.SerialNumbers);
            product.AddProductInstances(productInstances);

            var inventoryChange = InventoryChange.Create(InventoryChangeType.In, productInstances);

            await _inventoryChangeRepository.AddAsync(inventoryChange);

            return inventoryChange.Id.ToString();
        }
    }
}
