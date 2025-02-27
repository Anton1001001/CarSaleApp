using System.Reflection;
using Advert.Application.Interfaces;
using Advert.Application.Services;
using Advert.Domain.Interfaces;
using Advert.Infrastructure.GrpcClients;
using Advert.Infrastructure.Repositories;
using Advert.Infrastructure.Services;
using Car.GrpcService;
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
        
        services.Configure<NbrbApiConfig>(configuration.GetSection(nameof(NbrbApiConfig)));
        
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);

        services.AddGrpcClient<CarCatalog.CarCatalogClient>(options =>
            options.Address = new Uri("http://localhost:5047"));
        services.AddScoped<ICarCatalogGrpcClient, CarCatalogGrpcClient>();
        services.AddSingleton<ICurrencyRateService, CurrencyRateService>();
        services.AddDbContext<AdvertDbContext>(options => options.UseMySql(connectionString, serverVersion));
        services.AddScoped<IAdvertRepository, AdvertRepository>();
        services.AddScoped<IAdvertPublicStatusRepository, AdvertPublicStatusRepository>();
        services.AddScoped<IAdvertPrivateStatusRepository, AdvertPrivateStatusRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}