using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;

namespace InventoryManagement.Application.CommandHandlers
{
    public sealed class CreationProductCommandHandler : IRequestHandler<CreationProductCommand, long>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreationProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreationProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.BrandName, request.ProductType);
            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
