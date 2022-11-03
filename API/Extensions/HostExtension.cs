using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class HostExtension
{
    public static async Task<IHost> EnsureDatabaseCreated(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        try
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();
            await Seed.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
            logger?.LogError(ex, "An error occured during migration");
        }

        return host;
    }
}