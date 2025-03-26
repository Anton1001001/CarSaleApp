using System.Reflection;
using Advert.Application.Abstractions;
using Advert.Application.Helpers;
using Advert.Application.Services;
using Advert.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddChainOfResponsibilityServices();
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        
        services.AddScoped<IAdvertService, AdvertService>();
        services.AddSingleton<ICurrencyConverter, CurrencyConverter>();
        
        return services;
    }
}