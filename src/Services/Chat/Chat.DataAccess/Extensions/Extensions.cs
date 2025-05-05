using System.Reflection;
using Chat.Core.Abstractions;
using Chat.DataAccess.GrpcClients;
using Chat.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.DataAccess.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ChatDb");
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        
        services.AddDbContext<ChatDbContext>(options =>
            options.UseMySql(connectionString, serverVersion));
        
        services.AddGrpcClient<Advert.GrpcService.Advert.AdvertClient>(options => 
            options.Address = new Uri("http://localhost:5017"));
        
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped<IAdvertServiceClient, AdvertServiceClient>();
        services.AddScoped<IDialogRepository, DialogRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}