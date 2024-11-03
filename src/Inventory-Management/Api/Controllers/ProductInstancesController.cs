using Inventory_Management.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Api.Controllers
{

    [ApiController]
    [Route("api/product/instances")]
    public class ProductInstancesController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddProductInstancesAsync(ProductInstanceInventoryCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

    }
}
