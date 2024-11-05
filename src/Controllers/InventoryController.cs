using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Queries;
using InventoryManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{

    [ApiController]
    [Route("api/inventory")]
    public class InventoryController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost("entries")]
        public async Task<IActionResult> RecordInventoryEntryAsync(InventoryEntryRequest request)
        {
            var commandRequest = new InventoryEntryCommand(request.BrandName, request.ProductType, request.SerialNumbers);
            var commandResponse = await _mediator.Send(commandRequest);
            var result = new InventoryEntryResponse(commandResponse);
            return Ok(result);
        }

        [HttpPost("exits")]
        public async Task<IActionResult> RecordInventoryExistAsync(IEnumerable<InventoryExistRequest> request)
        {
            var commandRequest = new InventoryExistCommand(request.Select(x => new InventoryExistItem(x.ProductId, x.Quantity)));
            var commandResponse = await _mediator.Send(commandRequest);
            var result = new InventoryExistResponse(commandResponse);
            return Ok(result);
        }

        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetProductInventoryAsync(Guid inventoryId)
        {
            var queryRequest = new InventoryDetailsQuery(inventoryId);
            var result = await _mediator.Send(queryRequest);
            return Ok(result);
        }

    }
}
