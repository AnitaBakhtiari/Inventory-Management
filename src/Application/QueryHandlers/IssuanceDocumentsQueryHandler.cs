using InventoryManagement.Application.Queries;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.Products;
using MediatR;
using System.Net;
using InventoryManagement.Models;
using InventoryManagement.Domain.IssuanceDocuments;

namespace InventoryManagement.Application.QueryHandlers
{
    public sealed class IssuanceDocumentsQueryHandler : IRequestHandler<IssuanceDocumentsQuery, IssuanceDocumentsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IIssuanceDocumentRepository _IssuanceDocumentRepository;

        public IssuanceDocumentsQueryHandler(IProductRepository productRepository, IIssuanceDocumentRepository IssuanceDocumentRepository)
        {
            _productRepository = productRepository;
            _IssuanceDocumentRepository = IssuanceDocumentRepository;
        }


        public async Task<IssuanceDocumentsResponse> Handle(IssuanceDocumentsQuery request, CancellationToken cancellationToken)
        {
            var IssuanceDocument = await _IssuanceDocumentRepository.GetByIdAsync(request.IssuanceDocumentId) ??
                throw new BusinessException(ExceptionMessages.IssuanceDocumentNotFound, (int)HttpStatusCode.NotFound);

            var productInvoiceItems = IssuanceDocument.ProductInstances
                .GroupBy(x => x.ProductId)
                .Select(group => new IssuanceDocumentsItem
                  (
                      ProductId: group.Key,
                      Quantity: group.Count(),
                      ProductBrandName: group.FirstOrDefault()?.Product?.BrandName ?? "Unknown"

                  )).ToArray();

            return new IssuanceDocumentsResponse(IssuanceDocumentId: request.IssuanceDocumentId, Items: productInvoiceItems);
        }
    }
}
