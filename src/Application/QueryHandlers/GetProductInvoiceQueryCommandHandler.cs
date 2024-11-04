using InventoryManagement.Application.Queries;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using MediatR;
using System.Net;
using InventoryManagement.Models;

namespace InventoryManagement.Application.QueryHandlers
{
    public sealed class GetProductInvoiceQueryHandler : IRequestHandler<ProductInvoiceQuery, ProductInvoiceDetailsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;

        public GetProductInvoiceQueryHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
        }


        public async Task<ProductInvoiceDetailsResponse> Handle(ProductInvoiceQuery request, CancellationToken cancellationToken)
        {
            var inventoryChange = await _inventoryChangeRepository.GetByIdAsync(request.InventoryChangeId) ??
                throw new BusinessException(ExceptionMessages.InventoryChangeNotFound, (int)HttpStatusCode.NotFound);

            var productInvoiceItems = inventoryChange.ProductInstances
                .GroupBy(x => x.ProductId)
                .Select(group => new ProductInvoiceItem
                  (
                      ProductId: group.Key,
                      Quantity: group.Count(),
                      ProductBrandName: group.FirstOrDefault()?.Product?.BrandName ?? "Unknown"

                  )).ToArray();

            return new ProductInvoiceDetailsResponse(InventoryChangeId: request.InventoryChangeId, ProductInvoiceItems: productInvoiceItems);
        }
    }
}
