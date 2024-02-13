using FoodStock.Application.Repositories;
using FoodStock.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStock.Infrastructure.DAL;

internal static class Extensions
{
    private const string SectionName = "Postgres";
    
    public static IServiceCollection AddPostgres(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(SectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(SectionName);
        services.AddDbContext<FoodStockDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProducentRepository, ProducentRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddHostedService<DatabaseInitializer>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }
}
