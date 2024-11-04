using InventoryManagement.Domain.InventoryChanges;
using InventoryManagement.Domain.Products;
using InventoryManagement.Infrastructure.Behavior;
using InventoryManagement.Infrastructure.Domain.InventoryChangeConfiguration;
using InventoryManagement.Infrastructure.Domain.ProductConfiguration;

namespace InventoryManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryChangeRepository, InventoryChangeRepository>();

            services.AddMediatR(cf =>
            {
                cf.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cf.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

            return services;

        }
    }
}
