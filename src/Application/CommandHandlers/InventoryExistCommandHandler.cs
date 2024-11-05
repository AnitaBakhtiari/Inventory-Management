using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class InventoryExistCommandHandler : IRequestHandler<InventoryExistCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryExistCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository,IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(InventoryExistCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            List<ProductInstance> productInstances = new();

            foreach (var inventoryItem in request.InventoryExistItems)
            {
                var product = await _productRepository.GetByIdAsync(inventoryItem.ProductId) ?? throw new BusinessException(ExceptionMessages.ProductNotFound, (int)HttpStatusCode.NotFound);

                if (!product.HasInventory(inventoryItem.Quantity))
                {
                    throw new BusinessException(ExceptionMessages.OutOfInventory, (int)HttpStatusCode.PreconditionFailed);
                }

                var effectedProductInstances = product.ReduceProductInstancesInventory(inventoryItem.Quantity);
                productInstances.AddRange(effectedProductInstances);
            }

            var inventoryChange = InventoryChange.CreateExit(productInstances);
            await _inventoryChangeRepository.AddAsync(inventoryChange);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return inventoryChange.Id.ToString();

        }

        private static void ValidateCommandRequest(InventoryExistCommand request)
        {
            if (request.InventoryExistItems == null || !request.InventoryExistItems.Any())
                throw new BusinessException(ExceptionMessages.InventoryExistItemsIsRequired, (int)HttpStatusCode.BadRequest);

            foreach (var item in request.InventoryExistItems)
            {
                if (item.Quantity <= 0)
                {
                    throw new BusinessException(ExceptionMessages.QuantityGreaterThanZero, (int)HttpStatusCode.PreconditionFailed);
                }
            }
        }
    }
}
