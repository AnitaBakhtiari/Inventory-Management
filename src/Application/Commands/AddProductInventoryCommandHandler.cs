using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Product;
using InventoryManagement.Domain.Product.Products;
using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record ProductInstanceInventoryCommand(string BrandName, ProductType ProductType, int InventoryCount, List<string> SerialNumbers) : IRequest<string>;

    public class AddProductInstanceInventoryCommandHandler : IRequestHandler<ProductInstanceInventoryCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;

        public AddProductInstanceInventoryCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
        }

        public async Task<string> Handle(ProductInstanceInventoryCommand request, CancellationToken cancellationToken)
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
