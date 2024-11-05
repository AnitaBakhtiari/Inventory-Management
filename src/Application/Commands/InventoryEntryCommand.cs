using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record InventoryEntryCommand(string BrandName, ProductType ProductType, List<string> SerialNumbers) : IRequest<string>;

}
