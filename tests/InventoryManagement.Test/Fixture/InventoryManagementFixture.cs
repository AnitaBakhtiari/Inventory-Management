using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Extensions;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace InventoryManagement.Test.Fixture
{
    public class InventoryManagementFixture
    {
        private IServiceCollection _service;

        public InventoryManagementFixture Build(string databaseName)
        {

            var builder = new ConfigurationBuilder().Build();

            _service = new ServiceCollection()
                .AddDependencyInjections()
                .AddDbContext<InventoryManagementDbContext>(x => x.UseInMemoryDatabase(databaseName));

            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return _service.BuildServiceProvider().CreateScope().ServiceProvider;
        }

        public InventoryManagementFixture WithSeedData()
        {
            var serviceProvider = BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<InventoryManagementDbContext>();

            var products = CreateProducts();
            var productInstances = CreateProductInstances(products);
            var IssuanceDocuments = CreateIssuanceDocuments(productInstances);

            dbContext.AddRange(IssuanceDocuments);
            dbContext.AddRange(products);

            dbContext.SaveChanges();

            return this;

        }

        private static List<Product> CreateProducts()
        {
            return new List<Product>
        {
            new Product
            {
                Id = 101,
                BrandName = "BrandA",
                Type = ProductType.Laptop,
                CreatedAt = DateTime.Now
            },
            new Product
            {
                Id = 102,
                BrandName = "BrandB",
                Type = ProductType.Laptop,
                CreatedAt = DateTime.Now
            }
        };
        }

        private static List<ProductInstance> CreateProductInstances(List<Product> products)
        {
            var productInstances = new List<ProductInstance>
        {
            new ProductInstance
            {
                SerialNumber = "SN001",
                CreatedOn = DateTime.Now,
                IsAvailable = true,
                ProductId = products[0].Id,
                Product = products[0]
            },
            new ProductInstance
            {
                SerialNumber = "SN002",
                CreatedOn =DateTime.Now,
                IsAvailable = true,
                ProductId = products[0].Id,
                Product = products[0]
            },
            new ProductInstance
            {
                SerialNumber = "SN003",
                CreatedOn = DateTime.Now,
                IsAvailable = false,
                ProductId = products[1].Id,
                Product = products[1]
            }
        };

            products[0].AddProductInstances(productInstances.Take(2).ToList());
            products[1].AddProductInstances(productInstances.Skip(2).Take(1).ToList());

            return productInstances;
        }

        private static List<IssuanceDocument> CreateIssuanceDocuments(List<ProductInstance> productInstances)
        {
            var IssuanceDocumentIn = new IssuanceDocument
            {
                Id = Guid.NewGuid(),
                Type = IssuanceDocumentType.Entry,
                CreatedOn = DateTime.Now,
                ProductInstances = productInstances.ToList()
            };

            var IssuanceDocumentOut = new IssuanceDocument
            {
                Id = Guid.NewGuid(),
                Type = IssuanceDocumentType.Exit,
                CreatedOn = DateTime.Now,
                ProductInstances = new List<ProductInstance> { productInstances[2] }
            };

            return [IssuanceDocumentIn, IssuanceDocumentOut];
        }

    }
}


