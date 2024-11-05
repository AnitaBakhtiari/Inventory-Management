using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record CreationProductCommand(string BrandName, ProductType ProductType, List<string> SerialNumbers) : IRequest<long>;

}
