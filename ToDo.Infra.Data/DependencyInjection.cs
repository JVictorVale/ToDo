using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Infra.Data.Context;
using ToDo.Infra.Data.Repositories;

namespace ToDo.Infra.Data;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));
        
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentListRepository, AssignmentListRepository>();
    }
}