﻿using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Queries;
using InventoryManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{

    [ApiController]
    [Route("api/issuances")]
    public class IssuancesController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost("entry")]
        public async Task<IActionResult> RecordEntryIssuanceDocumentAsync(EntryIssuanceRequest request)
        {
            var commandRequest = new EntryIssuanceCommand(request.BrandName, request.ProductType, request.SerialNumbers);
            var commandResponse = await _mediator.Send(commandRequest);
            var result = new EntryIssuanceResponse(commandResponse);
            return Ok(result);
        }

        [HttpPost("exit")]
        public async Task<IActionResult> RecordExistIssuanceDocumentAsync(IEnumerable<ExistIssuanceRequest> request)
        {
            var commandRequest = new ExistIssuanceCommand(request.Select(x => new ExistIssuanceDocumentItem(x.ProductId, x.Quantity)));
            var commandResponse = await _mediator.Send(commandRequest);
            var result = new ExistIssuanceResponse(commandResponse);
            return Ok(result);
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RecordRefundIssuanceDocumentAsync(RefundIssuanceRequest request)
        {
            var commandRequest = new RefundIssuanceCommand(request.SerialNumber);
            var commandResponse = await _mediator.Send(commandRequest);
            var result = new RefundIssuanceResponse(commandResponse);
            return Ok(result);
        }

        [HttpGet("{issuanceId}")]
        public async Task<IActionResult> GetIssuanceDocumentsAsync(Guid issuanceId)
        {
            var queryRequest = new IssuanceDocumentsQuery(issuanceId);
            var result = await _mediator.Send(queryRequest);
            return Ok(result);
        }

    }
}
