using InventoryManagement.Models;
using MediatR;

namespace InventoryManagement.Application.Queries
{
    public record ProductInvoiceQuery(Guid InventoryChangeId) : IRequest<ProductInvoiceDetailsResponse>;
}
