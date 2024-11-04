using System.Net;
using InventoryManagement.Application.CommandHandlers;
using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using NSubstitute;

public class IssueProductExitInvoiceCommandHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryChangeRepository _inventoryChangeRepository;
    private readonly IssueProductExitInvoiceCommandHandler _sut;

    public IssueProductExitInvoiceCommandHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _inventoryChangeRepository = Substitute.For<IInventoryChangeRepository>();

        _sut = new IssueProductExitInvoiceCommandHandler(_productRepository, _inventoryChangeRepository);
    }

    [Fact]
    public async Task Handle_ShouldThrowBusinessException_WhenProductNotFound()
    {
        // Arrange
        var command = new ProductExitInvoiceCommand
        (
            ProductExitInvoiceItems: new List<ProductExitInvoiceItem>
            {
                new ( ProductId : 999, Quantity : 5 )
            }
        );

        _productRepository.GetByIdAsync(999).Returns((Product)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _sut.Handle(command, CancellationToken.None));

        Assert.Equal(ExceptionMessages.ProductNotFound, exception.Message);
        Assert.Equal((int)HttpStatusCode.NotFound, exception.ErrorCode);
    }

    [Fact]
    public async Task Handle_ShouldThrowBusinessException_WhenInsufficientInventory()
    {
        // Arrange
        var command = new ProductExitInvoiceCommand
         (
           ProductExitInvoiceItems: new List<ProductExitInvoiceItem>
           {
             new ( ProductId : 1, Quantity : 5)
           }
         );


        var product = Substitute.For<Product>();
        product.HasInventory(5).Returns(false);

        _productRepository.GetByIdAsync(1).Returns(product);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessException>(() => _sut.Handle(command, CancellationToken.None));

        Assert.Equal(ExceptionMessages.OutOfInventory, exception.Message);
        Assert.Equal((int)HttpStatusCode.PreconditionFailed, exception.ErrorCode);
    }
}
