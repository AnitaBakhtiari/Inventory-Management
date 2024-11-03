using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Product.Products;
using InventoryManagement.Infrastructure.Domain.InventoryChangeConfiguration;
using InventoryManagement.Infrastructure.Domain.ProductConfiguration;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryChangeRepository, InventoryChangeRepository>();

            services.AddDbContext<InventoryManagementDbContext>((sp, options) => options.UseSqlServer(configuration.GetConnectionString("SqlDB")));

            return services;

        }
    }
}
