using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Test.Fixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
namespace InventoryManagement.Test.Integration
{
    public class InventoryExistCommandHandlerTests
    {
        private readonly InventoryManagementDbContext _dbContext;


        private readonly IMediator _mediator;

        public InventoryExistCommandHandlerTests()
        {
            var serviceProvider = new InventoryManagementFixture()
                 .Build(Guid.NewGuid().ToString())
                 .WithSeedData()
                 .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<InventoryManagementDbContext>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldUnAvailableProductInstancesAndRecordInventoryChange_WhenValidExitRequest()
        {
            // Arrange
            var exitCommand = new InventoryExistCommand
            (
                ProductExitInvoiceItems: [new(ProductId: 101L, Quantity: 2)]
            );


            // Act
            var result = await _mediator.Send(exitCommand, CancellationToken.None);

            // Assert

            var inventoryChanges = await _dbContext.InventoryChanges.FindAsync(Guid.Parse(result));

            var ProductInstancesCount = inventoryChanges.ProductInstances.Count();

            Assert.NotNull(result);
            Assert.NotNull(inventoryChanges);
            Assert.True(inventoryChanges.Type == InventoryChangeType.Exit);
            Assert.True(ProductInstancesCount == exitCommand.ProductExitInvoiceItems.Sum(x => x.Quantity));
        }

        [Fact]
        public async Task Handle_ShouldThrowBusinessException_WhenInsufficientInventory()
        {
            // Arrange
            var exitCommand = new InventoryExistCommand
            (
                ProductExitInvoiceItems: [new(ProductId: 101L, Quantity: 2), new(ProductId: 102L, Quantity: 1)]
            );


            // Act && Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _mediator.Send(exitCommand, CancellationToken.None));

            Assert.Equal(ExceptionMessages.OutOfInventory, exception.Message);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, exception.ErrorCode);

        }

    }
}
