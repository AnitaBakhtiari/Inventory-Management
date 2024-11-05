using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.IssuanceDocuments;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Test.Fixture;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
namespace InventoryManagement.Test.Integration
{
    public class ExistIssuanceCommandHandlerTests
    {
        private readonly InventoryManagementDbContext _dbContext;


        private readonly IMediator _mediator;

        public ExistIssuanceCommandHandlerTests()
        {
            var serviceProvider = new InventoryManagementFixture()
                 .Build(Guid.NewGuid().ToString())
                 .WithSeedData()
                 .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<InventoryManagementDbContext>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldUnAvailableProductInstancesAndRecordIssuanceDocument_WhenValidExitRequest()
        {
            // Arrange
            var exitCommand = new ExistIssuanceCommand
            (
                ExistIssuanceDocumentItems: [new(ProductId: 101L, Quantity: 2)]
            );


            // Act
            var result = await _mediator.Send(exitCommand, CancellationToken.None);

            // Assert

            var IssuanceDocuments = await _dbContext.IssuanceDocuments.FindAsync(Guid.Parse(result));

            var ProductInstancesCount = IssuanceDocuments.ProductInstances.Count();

            Assert.NotNull(result);
            Assert.NotNull(IssuanceDocuments);
            Assert.True(IssuanceDocuments.Type == IssuanceDocumentType.Exit);
            Assert.True(ProductInstancesCount == exitCommand.ExistIssuanceDocumentItems.Sum(x => x.Quantity));
        }

        [Fact]
        public async Task Handle_ShouldThrowBusinessException_WhenInsufficientInventory()
        {
            // Arrange
            var exitCommand = new ExistIssuanceCommand
            (
                ExistIssuanceDocumentItems: [new(ProductId: 101L, Quantity: 2), new(ProductId: 102L, Quantity: 1)]
            );


            // Act && Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _mediator.Send(exitCommand, CancellationToken.None));

            Assert.Equal(ExceptionMessages.OutOfInventory, exception.Message);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, exception.ErrorCode);

        }

    }
}
