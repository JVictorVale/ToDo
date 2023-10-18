using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ScottBrady91.AspNetCore.Identity;
using ToDo.Application.Contracts;
using ToDo.Application.Notification;
using ToDo.Application.Services;
using ToDo.Domain.Models;

namespace ToDo.Application.Configurations;

public static class DependencyConfig
{
    public static void ResolveDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton(_ => builder.Configuration);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IAssignmentListService, AssignmentListService>();

        services.AddScoped<INotificator, Notificator>();
        services.AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();
    }
}