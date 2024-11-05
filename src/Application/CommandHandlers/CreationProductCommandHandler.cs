using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.CommandHandlers
{
    public sealed class CreationProductCommandHandler : IRequestHandler<CreationProductCommand, long>
    {
        private readonly IProductRepository _productRepository;

        public CreationProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<long> Handle(CreationProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.BrandName, request.ProductType);
            await _productRepository.AddAsync(product);

            return product.Id;
        }
    }
}
