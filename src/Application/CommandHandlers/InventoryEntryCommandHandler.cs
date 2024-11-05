using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class InventoryEntryCommandHandler : IRequestHandler<InventoryEntryCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;
        private readonly IMediator _mediator;

        public InventoryEntryCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository, IMediator mediator)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
            _mediator = mediator;
        }

        public async Task<string> Handle(InventoryEntryCommand request, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetByBrandNameAndProductTypeAsync(request.BrandName, request.ProductType);

            if (product == null)
            {
                var creationProductId = await _mediator.Send(new CreationProductCommand(request.BrandName, request.ProductType, request.SerialNumbers), cancellationToken);
                product = await _productRepository.GetByIdAsync((long)creationProductId);
            }

            var productInstances = ProductInstance.Create(request.SerialNumbers);

            product.AddProductInstances(productInstances);

            var inventoryChange = InventoryChange.CreateEntry(productInstances);

            await _inventoryChangeRepository.AddAsync(inventoryChange);

            return inventoryChange.Id.ToString();
        }
    }
}
