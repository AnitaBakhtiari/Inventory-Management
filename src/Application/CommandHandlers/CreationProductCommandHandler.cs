using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using MediatR;

namespace InventoryManagement.Application.CommandHandlers
{
    public sealed class CreationProductCommandHandler(IInventoryManagementUnitOfWork unitOfWork) : IRequestHandler<CreationProductCommand, long>
    {

        private readonly IInventoryManagementUnitOfWork _unitOfWork = unitOfWork;

        public async Task<long> Handle(CreationProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.BrandName, request.ProductType);
            await _unitOfWork.ProductRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
