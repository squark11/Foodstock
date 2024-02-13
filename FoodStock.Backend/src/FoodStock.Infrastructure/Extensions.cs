using FluentValidation;
using FoodStock.Core.Entities;
using FoodStock.Infrastructure.Authentication;
using FoodStock.Infrastructure.Authentication.DTO;
using FoodStock.Infrastructure.Authentication.Services;
using FoodStock.Infrastructure.DAL;
using FoodStock.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStock.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        services.AddSingleton<ExceptionMiddleware>();
        services.AddControllers();
        services.AddPostgres(configuration);
        services.AddAuth(configuration);
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}
