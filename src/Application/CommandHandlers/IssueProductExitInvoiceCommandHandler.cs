﻿using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using MediatR;
using System.Net;

namespace InventoryManagement.Application.CommandHandlers
{

    public sealed class IssueProductExitInvoiceCommandHandler : IRequestHandler<ProductExitInvoiceCommand, string>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryChangeRepository _inventoryChangeRepository;

        public IssueProductExitInvoiceCommandHandler(IProductRepository productRepository, IInventoryChangeRepository inventoryChangeRepository)
        {
            _productRepository = productRepository;
            _inventoryChangeRepository = inventoryChangeRepository;
        }

        public async Task<string> Handle(ProductExitInvoiceCommand requests, CancellationToken cancellationToken)
        {
            List<ProductInstance> productInstances = new();

            foreach (var request in requests.ProductExitInvoiceItems)
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new BusinessException(ExceptionMessages.ProductNotFound, (int)HttpStatusCode.NotFound);

                if (!product.HasInventory(request.Quantity))
                {
                    throw new BusinessException(ExceptionMessages.OutOfInventory, (int)HttpStatusCode.PreconditionFailed);
                }

                var effectedProductInstances = product.ReduceProductInstancesInventory(request.Quantity);
                productInstances.AddRange(effectedProductInstances);
            }

            var inventoryChange = InventoryChange.Create(InventoryChangeType.Out, productInstances);
            await _inventoryChangeRepository.AddAsync(inventoryChange);

            return inventoryChange.Id.ToString();

        }
    }
}