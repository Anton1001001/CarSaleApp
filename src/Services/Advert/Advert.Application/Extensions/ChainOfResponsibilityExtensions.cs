using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateBusAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;
using Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateMotoAdvert;
using Advert.Application.CQRS.Queries.GetAdvertForm;
using Advert.Application.CQRS.Queries.GetAdvertForm.Processors.GetCarAdvertForm;
using Advert.Application.Services.Processors.GetCarAdvertById;
using Advert.Application.Services.Processors.GetCarAdvertPreviewById;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Extensions;

public static class ChainOfResponsibilityExtensions
{
    public static IServiceCollection AddChainOfResponsibilityServices(this IServiceCollection services)
    {
        services.AddScoped<CreateCarAdvertProcessor>();
        services.AddScoped<CreateBusAdvertProcessor>();
        services.AddScoped<CreateMotoAdvertProcessor>();

        services.AddScoped<Processor<CreateAdvertCommand, Result<AdvertResponse>>>(sp =>
        {
            var carHandler = sp.GetRequiredService<CreateCarAdvertProcessor>();
            var busHandler = sp.GetRequiredService<CreateBusAdvertProcessor>();
            var motoHandler = sp.GetRequiredService<CreateMotoAdvertProcessor>();

            carHandler
                .SetNext(busHandler)
                .SetNext(motoHandler);
            return carHandler;
        });

        services.AddScoped<GetCarAdvertFormProcessor>();

        services.AddScoped<Processor<GetAdvertFormQuery, Result<GetAdvertFormResponse>>>(sp =>
        {
            var carHandler = sp.GetRequiredService<GetCarAdvertFormProcessor>();

            return carHandler;
        });

        services.AddScoped<GetCarAdvertByIdProcessor>();
        
        services.AddScoped<Processor<Domain.Entities.Advert, Result<AdvertResponse>>>(sp =>
        {
            var carHandler = sp.GetRequiredService<GetCarAdvertByIdProcessor>();

            return carHandler;
        });

        services.AddScoped<GetCarAdvertPreviewById>();

        services.AddScoped<Processor<Domain.Entities.Advert, Result<AdvertPreviewResponse>>>(sp =>
        {
            var carHandler = sp.GetRequiredService<GetCarAdvertPreviewById>();

            return carHandler;
        });
        
        return services;
    }
}