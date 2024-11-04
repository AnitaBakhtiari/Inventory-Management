using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record ProductExitInvoiceCommand(IEnumerable<ProductExitInvoiceItem> ProductExitInvoiceItems) : IRequest<string>;
    public record ProductExitInvoiceItem(long ProductId, int Quantity);

}
