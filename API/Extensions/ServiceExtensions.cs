using Application.Activities;
using Domain;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ServiceExtensions
{
    private static IServiceCollection AddMapster(this IServiceCollection services, Action<TypeAdapterConfig>? options = null)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Default.IgnoreNullValues(true);

        options?.Invoke(config);
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("Sqlite"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });
        });

        services.AddMediatR(typeof(List).Assembly);
        services.AddMapster();

        return services;
    }
}