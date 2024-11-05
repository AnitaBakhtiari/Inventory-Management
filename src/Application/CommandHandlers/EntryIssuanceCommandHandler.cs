using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class EntryIssuanceCommandHandler : IRequestHandler<EntryIssuanceCommand, string>
    {

        private readonly IInventoryManagementUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public EntryIssuanceCommandHandler(IMediator mediator, IInventoryManagementUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(EntryIssuanceCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            var product = await _unitOfWork.ProductRepository.GetByBrandNameAndProductTypeAsync(request.BrandName, request.ProductType);

            if (product == null)
            {
                var creationProductId = await _mediator.Send(new CreationProductCommand(request.BrandName, request.ProductType, request.SerialNumbers), cancellationToken);
                product = await _unitOfWork.ProductRepository.GetByIdAsync(creationProductId);
            }

            var productInstances = ProductInstance.Create(request.SerialNumbers);

            product.AddProductInstances(productInstances);

            var issuanceDocument = IssuanceDocument.CreateEntry(productInstances);

            await _unitOfWork.IssuanceDocumentRepository.AddAsync(issuanceDocument);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return issuanceDocument.Id.ToString();
        }

        private static void ValidateCommandRequest(EntryIssuanceCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.BrandName))
                throw new BusinessException(ExceptionMessages.BrandNameIsRequired, (int)HttpStatusCode.BadRequest);

            if (request.SerialNumbers == null || !request.SerialNumbers.Any())
                throw new BusinessException(ExceptionMessages.SerialNumbersIsRequired, (int)HttpStatusCode.BadRequest);
        }
    }
}
