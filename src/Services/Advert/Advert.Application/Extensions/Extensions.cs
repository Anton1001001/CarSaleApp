using System.Reflection;
using Advert.Application.Abstractions;
using Advert.Application.Helpers;
using Advert.Application.Options;
using Advert.Application.Services;
using Advert.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageQueueOptions>(configuration.GetSection(nameof(MessageQueueOptions)));
        services.AddChainOfResponsibilityServices();
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped<IPhotosSenderService, PhotosSenderService>();
        services.AddTransient<IAdvertService, AdvertService>();
        services.AddSingleton<ICurrencyConverter, CurrencyConverter>();
        
        return services;
    }
}