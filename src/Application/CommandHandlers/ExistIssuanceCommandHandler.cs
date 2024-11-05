using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class ExistIssuanceCommandHandler(IInventoryManagementUnitOfWork unitOfWork) : IRequestHandler<ExistIssuanceCommand, string>
    {

        private readonly IInventoryManagementUnitOfWork _unitOfWork = unitOfWork;

        public async Task<string> Handle(ExistIssuanceCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            List<ProductInstance> productInstances = new();

            foreach (var IssuanceDocumentItem in request.ExistIssuanceDocumentItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(IssuanceDocumentItem.ProductId) ?? throw new BusinessException(ExceptionMessages.ProductNotFound, (int)HttpStatusCode.NotFound);

                if (!product.HasInventory(IssuanceDocumentItem.Quantity))
                {
                    throw new BusinessException(ExceptionMessages.OutOfInventory, (int)HttpStatusCode.PreconditionFailed);
                }

                var effectedProductInstances = product.ReduceProductInstancesInventory(IssuanceDocumentItem.Quantity);
                productInstances.AddRange(effectedProductInstances);
            }

            var issuanceDocument = IssuanceDocument.CreateExit(productInstances);
            await _unitOfWork.IssuanceDocumentRepository.AddAsync(issuanceDocument);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return issuanceDocument.Id.ToString();

        }

        private static void ValidateCommandRequest(ExistIssuanceCommand request)
        {
            if (request.ExistIssuanceDocumentItems == null || !request.ExistIssuanceDocumentItems.Any())
                throw new BusinessException(ExceptionMessages.ExistIssuanceDocumentItemsIsRequired, (int)HttpStatusCode.BadRequest);

            foreach (var item in request.ExistIssuanceDocumentItems)
            {
                if (item.Quantity <= 0)
                {
                    throw new BusinessException(ExceptionMessages.QuantityGreaterThanZero, (int)HttpStatusCode.BadRequest);
                }
            }
        }
    }
}
