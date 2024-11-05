using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record InventoryExistCommand(IEnumerable<InventoryExistItem> ProductExitInvoiceItems) : IRequest<string>;
    public record InventoryExistItem(long ProductId, int Quantity);

}
