using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{
    public class RefundIssuanceCommandHandler(IInventoryManagementUnitOfWork unitOfWork) : IRequestHandler<RefundIssuanceCommand, string>
    {

        private readonly IInventoryManagementUnitOfWork _unitOfWork = unitOfWork;

        public async Task<string> Handle(RefundIssuanceCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            var (product, productInstance) = await _unitOfWork.ProductRepository.GetBySerialNumberAsync(request.SerialNumber);

            if (product == null)
            {
                throw new BusinessException(ExceptionMessages.ProductSerialNumberNotFound, (int)HttpStatusCode.NotFound);
            }

            if (productInstance.IsAvailable)
            {
                throw new BusinessException(ExceptionMessages.productInstanceIsExist, (int)HttpStatusCode.NotFound);
            }

            product.IncreaseProductInstanceInventory([productInstance.SerialNumber]);

            var issuanceDocument = IssuanceDocument.CreateRefund([productInstance]);

            await _unitOfWork.IssuanceDocumentRepository.AddAsync(issuanceDocument);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return issuanceDocument.Id.ToString();
        }

        private static void ValidateCommandRequest(RefundIssuanceCommand request)
        {

            if (string.IsNullOrWhiteSpace(request.SerialNumber))
                throw new BusinessException(ExceptionMessages.SerialNumbersIsRequired, (int)HttpStatusCode.BadRequest);
        }
    }
}
