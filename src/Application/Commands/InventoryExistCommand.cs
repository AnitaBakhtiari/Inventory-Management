using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record InventoryExistCommand(IEnumerable<InventoryExistItem> InventoryExistItems) : IRequest<string>;
    public record InventoryExistItem(long ProductId, int Quantity);

}
