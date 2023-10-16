using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
}