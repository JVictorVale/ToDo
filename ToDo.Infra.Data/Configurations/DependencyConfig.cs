using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Infra.Data.Repositories;

namespace ToDo.Infra.Data.Configurations;

public static class DependencyConfig
{
    public static void ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentListRepository, AssignmentListRepository>();
    }
}