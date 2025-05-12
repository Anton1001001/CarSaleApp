using System.Reflection;
using Advert.Application.Abstractions;
using Advert.Application.Abstractions.GrpcClients;
using Advert.Application.JobManaging;
using Advert.Domain.Entities;
using Advert.Domain.Interfaces.Repositories;
using Advert.Infrastructure.GrpcClients.CarsCatalog;
using Advert.Infrastructure.GrpcClients.FileService;
using Advert.Infrastructure.Hangfire;
using Advert.Infrastructure.Messaging;
using Advert.Infrastructure.Options;
using Advert.Infrastructure.Repositories;
using Advert.Infrastructure.Services;
using Car.GrpcService;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Advert.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AdvertDb")!;
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>();
        
        services.Configure<NbrbApiConfig>(configuration.GetSection(nameof(NbrbApiConfig)));
        services.Configure<PhotosIdsSendingJobOptions>(configuration.GetSection(nameof(PhotosIdsSendingJobOptions)));
        
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);

        services.AddGrpcClient<CarCatalog.CarCatalogClient>(options =>
            options.Address = new Uri("http://car-grpc:5047"));
        
        services.AddGrpcClient<File.GrpcService.File.FileClient>(options => 
            options.Address = new Uri("http://file-grpc:5086"));

        services.AddScoped<IFileServiceGrpcClient, FileServiceGrpcClient>();
        services.AddScoped<ICarCatalogGrpcClient, CarCatalogGrpcClient>();
        
        var redisConnectionString = configuration.GetConnectionString("Redis")!;
        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRepository<Place>, Repository<Place>>();
        services.Decorate<IRepository<Place>, CachedRepository<Place>>();
        
        services.AddScoped<IRepository<PhoneCode>, Repository<PhoneCode>>();
        services.Decorate<IRepository<PhoneCode>, CachedRepository<PhoneCode>>();
        
        services.AddSingleton<ICurrencyRateService, CurrencyRateService>();
        services.AddDbContext<AdvertDbContext>(options => options.UseMySql(connectionString, serverVersion));
        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<IAdvertRepository, AdvertRepository>();
        services.AddScoped<IAdvertPublicStatusRepository, AdvertPublicStatusRepository>();
        services.AddScoped<IAdvertPrivateStatusRepository, AdvertPrivateStatusRepository>();
        services.AddScoped<IAdvertCategoryRepository, AdvertCategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHangfire(options =>
        {
            options.UseRedisStorage(redisConnectionString, new RedisStorageOptions
            {
                Prefix = "hangfire:",
                InvisibilityTimeout = TimeSpan.FromMinutes(5)
            });
        });

        services.AddHangfireServer();
        
        services.AddSingleton<IPhotosIdsSendingJob, HangfirePhotosIdsSendingJob>();
        
        return services;
    }
}