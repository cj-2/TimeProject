using Microsoft.EntityFrameworkCore;
using TimeProject.Infrastructure.Database;

namespace TimeProject.APIs.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<CustomDbContext>(options => options.UseNpgsql(dbConnectionString));
        return services;
    }
}