using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{
    public class RefundIssuanceCommandHandler : IRequestHandler<RefundIssuanceCommand, string>
    {
        private readonly IIssuanceDocumentRepository _IssuanceDocumentRepository;
        private readonly IProductInstanceRepository _productInstanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RefundIssuanceCommandHandler(
            IIssuanceDocumentRepository IssuanceDocumentRepository,
            IUnitOfWork unitOfWork,
            IProductInstanceRepository productInstanceRepository)
        {

            _IssuanceDocumentRepository = IssuanceDocumentRepository;
            _unitOfWork = unitOfWork;
            _productInstanceRepository = productInstanceRepository;
        }

        public async Task<string> Handle(RefundIssuanceCommand request, CancellationToken cancellationToken)
        {
            ValidateCommandRequest(request);

            var productInstance = await _productInstanceRepository.GetBySerialNumberAsync(request.SerialNumber)
                   ?? throw new BusinessException(ExceptionMessages.ProductSerialNumberNotFound, (int)HttpStatusCode.NotFound);

            if (productInstance.IsAvailable)
            {
                throw new BusinessException(ExceptionMessages.productInstanceIsExist, (int)HttpStatusCode.NotFound);
            }

            var product = productInstance.Product;

            product.IncreaseProductInstanceInventory([productInstance.SerialNumber]);

            var issuanceDocument = IssuanceDocument.CreateRefund([productInstance]);

            await _IssuanceDocumentRepository.AddAsync(issuanceDocument);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return issuanceDocument.Id.ToString();
        }

        private static void ValidateCommandRequest(RefundIssuanceCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.BrandName))
                throw new BusinessException(ExceptionMessages.BrandNameIsRequired, (int)HttpStatusCode.BadRequest);

            if (string.IsNullOrWhiteSpace(request.SerialNumber))
                throw new BusinessException(ExceptionMessages.SerialNumbersIsRequired, (int)HttpStatusCode.BadRequest);
        }
    }
}
