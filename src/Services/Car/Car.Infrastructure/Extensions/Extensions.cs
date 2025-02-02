namespace Car.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CarInfoDb");
        var serverVersion = ServerVersion.Parse(configuration.GetSection("DatabaseOptions:ServerVersion").Value);
        services.AddDbContext<CarInfoDbContext>(options => options.UseMySql(connectionString, serverVersion));
        services.AddMigration<CarInfoDbContext, CarInfoDbContextSeed>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICarBrandRepository, CarBrandRepository>();
        services.AddScoped<ICarModelRepository, CarModelRepository>();
        services.AddScoped<ICarGenerationRepository, CarGenerationRepository>();
        services.AddScoped<ICarModificationRepository, CarModificationRepository>();
        return services;
    }
}