using InventoryManagement.Models;
using MediatR;

namespace InventoryManagement.Application.Queries
{
    public record InventoryDetailsQuery(Guid InventoryChangeId) : IRequest<InventoryDetailsResponse>;
}
