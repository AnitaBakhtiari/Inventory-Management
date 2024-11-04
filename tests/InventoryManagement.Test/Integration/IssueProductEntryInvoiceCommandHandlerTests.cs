using InventoryManagement.Application.Commands;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure;
using InventoryManagement.Test.Fixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace InventoryManagement.Test.Integration
{
    public class IssueProductEntryInvoiceCommandHandlerTests
    {
        private readonly InventoryManagementDbContext _dbContext;
        private readonly IMediator _mediator;

        public IssueProductEntryInvoiceCommandHandlerTests()
        {
            var serviceProvider = new InventoryManagementFixture()
                .Build(Guid.NewGuid().ToString())
                .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<InventoryManagementDbContext>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldAddNewProductAndProductInstances_WhenProductDoesNotExist()
        {
            // Arrange
            var command = new ProductEntryInvoiceCommand
            (
                BrandName: "NewBrand",
                ProductType: ProductType.Laptop,
                SerialNumbers: ["SN1001", "SN1002", "SN1003", "SN1004"]
            );

            // Act
            var result = await _mediator.Send(command, CancellationToken.None);

            // Assert
            var product = _dbContext.Products
                  .Include(x => x.ProductInstances)
                  .FirstOrDefault(x => x.BrandName == command.BrandName && x.Type == command.ProductType);

            Assert.NotNull(product);
            Assert.Equal(product.ProductInstances.Where(x => x.IsAvailable).Count(), command.SerialNumbers.Count());
            Assert.True(Guid.TryParse(result, out _), "The returned result should be a valid GUID string.");
        }
    }

}
