using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record ExistIssuanceCommand(IEnumerable<ExistIssuanceDocumentItem> ExistIssuanceDocumentItems) : IRequest<string>;
    public record ExistIssuanceDocumentItem(long ProductId, int Quantity);

}
