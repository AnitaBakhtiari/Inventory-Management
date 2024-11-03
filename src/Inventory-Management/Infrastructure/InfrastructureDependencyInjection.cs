using Inventory_Management.Domain.InventoryChanges;
using Inventory_Management.Domain.Product.Products;
using Inventory_Management.Infrastructure.Domain.InventoryChangeConfiguration;
using Inventory_Management.Infrastructure.Domain.ProductConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management.Infrastructure
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
