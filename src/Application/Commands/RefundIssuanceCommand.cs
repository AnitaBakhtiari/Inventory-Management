using InventoryManagement.Domain.Products;
using MediatR;

namespace InventoryManagement.Application.Commands
{
    public record RefundIssuanceCommand(string SerialNumber): IRequest<string>;

}
