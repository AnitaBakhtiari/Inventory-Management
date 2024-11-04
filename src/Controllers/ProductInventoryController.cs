using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Queries;
using InventoryManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{

    [ApiController]
    [Route("api/products/inventory")]
    public class ProductInventoryController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost("in")]
        public async Task<IActionResult> AddProductInventoryAsync(ProductEntryInvoiceRequest request)
        {
            var commandRequest = new ProductEntryInvoiceRequest(request.BrandName, request.ProductType, request.SerialNumbers);
            await _mediator.Send(commandRequest);
            return Ok();
        }

        [HttpPost("out")]
        public async Task<IActionResult> AddProductInventoryAsync(IEnumerable<ProductExitInvoiceRequest> request)
        {
            var commandRequest = new ProductExitInvoiceCommand(request.Select(x => new ProductExitInvoiceItem(x.ProductId, x.Quantity)));
            await _mediator.Send(commandRequest);
            return Ok();
        }

        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetProductInventoryAsync(Guid inventoryId)
        {
            var queryRequest = new ProductInvoiceQuery(inventoryId);
            var result = await _mediator.Send(queryRequest);
            return Ok(result);
        }

    }
}
