using Amazon.Runtime;
using Amazon.S3;
using File.Core.Abstractions;
using File.DataAccess.Options;
using File.DataAccess.Repositories;
using File.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace File.DataAccess.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["FileDb"];
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<FileDbContext>(options => options
            .UseMySql(connectionString, serverVersion)           
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()); 

        services.Configure<BackblazeOptions>(configuration.GetSection(nameof(BackblazeOptions)));

        services.AddSingleton<IAmazonS3>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<BackblazeOptions>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                ForcePathStyle = true
            };
            var credentials = new BasicAWSCredentials(options.KeyId, options.ApplicationKey);
            return new AmazonS3Client(credentials, config);
        });
        services.AddScoped<IStorageService, BackblazeB2Service>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}