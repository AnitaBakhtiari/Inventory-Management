using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Persistence.Domain.InventoryChangeConfiguration;
using InventoryManagement.Infrastructure.Persistence.Domain.ProductConfiguration;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagement.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryChangeRepository, InventoryChangeRepository>();

            services.AddMediatR(cf =>
            {
                cf.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return services;

        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InventoryManagementDbContext>((sp, options) => options.UseSqlServer(configuration.GetConnectionString("SqlDB")));
            return services;

        }
    }
}
