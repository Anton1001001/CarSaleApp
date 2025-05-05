using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Core.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        
        return services;
    }
}