using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class InventoryEntryCommandHandler : IRequestHandler<InventoryEntryCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryEntryCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(InventoryEntryCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            var product = await _productRepository.GetByBrandNameAndProductTypeAsync(request.BrandName, request.ProductType);

            if (product == null)
            {
                var creationProductId = await _mediator.Send(new CreationProductCommand(request.BrandName, request.ProductType, request.SerialNumbers), cancellationToken);
                product = await _productRepository.GetByIdAsync(creationProductId);
            }

            var productInstances = ProductInstance.Create(request.SerialNumbers);

            product.AddProductInstances(productInstances);

            var inventoryChange = InventoryChange.CreateEntry(productInstances);

            await _inventoryChangeRepository.AddAsync(inventoryChange);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return inventoryChange.Id.ToString();
        }

        private static void ValidateCommandRequest(InventoryEntryCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.BrandName))
                throw new BusinessException(ExceptionMessages.BrandNameIsRequired, (int)HttpStatusCode.BadRequest);

            if (request.SerialNumbers == null || !request.SerialNumbers.Any())
                throw new BusinessException(ExceptionMessages.SerialNumbersIsRequired, (int)HttpStatusCode.BadRequest);
        }
    }
}
