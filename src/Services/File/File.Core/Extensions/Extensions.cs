using System.Reflection;
using File.Core.Messaging;
using File.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace File.Core.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MessageQueueOptions>(configuration.GetSection(nameof(MessageQueueOptions)));
        services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(RabbitMqOptions)));

        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

        return services;
    }
}