using InventoryManagement.Models;
using MediatR;

namespace InventoryManagement.Application.Queries
{
    public record IssuanceDocumentsQuery(Guid IssuanceDocumentId) : IRequest<IssuanceDocumentsResponse>;
}
