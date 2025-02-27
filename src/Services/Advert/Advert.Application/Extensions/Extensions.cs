using System.Reflection;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateBusAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateMotoAdvert;
using Advert.Application.CQRS.Queries.GetAdvertById.Processors.GetCarAdvertById;
using Advert.Application.Helpers;
using Advert.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CreateCarAdvertProcessor>();
        services.AddScoped<CreateBusAdvertProcessor>();
        services.AddScoped<CreateMotoAdvertProcessor>();

        services.AddScoped<Processor<CreateAdvertCommand, AdvertResponse>>(sp =>
        {
            var carHandler = sp.GetRequiredService<CreateCarAdvertProcessor>();
            var busHandler = sp.GetRequiredService<CreateBusAdvertProcessor>();
            var motoHandler = sp.GetRequiredService<CreateMotoAdvertProcessor>();

            carHandler
                .SetNext(busHandler)
                .SetNext(motoHandler);
            return carHandler;
        });

        services.AddScoped<GetCarAdvertByIdProcessor>();
        
        services.AddScoped<Processor<Domain.Entities.Advert, AdvertResponse>>(sp =>
        {
            var carHandler = sp.GetRequiredService<GetCarAdvertByIdProcessor>();

            return carHandler;
        });

        services.AddSingleton<ICurrencyConverter, CurrencyConverter>();

        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        return services;
    }
}