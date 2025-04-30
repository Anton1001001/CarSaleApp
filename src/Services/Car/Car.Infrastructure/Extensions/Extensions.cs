namespace Car.Infrastructure.Extensions;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["CarInfoDb"];
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<CarInfoDbContext>(options => options.UseMySql(connectionString, serverVersion));
        // services.AddMigration<CarInfoDbContext, CarInfoDbContextSeed>();

        var redisConnectionString = configuration["Redis"];
        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRepository<CarBrand>, Repository<CarBrand>>();
        services.Decorate<IRepository<CarBrand>, CachedRepository<CarBrand>>();

        services.AddScoped<IRepository<CarColor>, Repository<CarColor>>();
        services.Decorate<IRepository<CarColor>, CachedRepository<CarColor>>();
        
        services.AddScoped<IRepository<CarInteriorMaterial>, Repository<CarInteriorMaterial>>();
        services.Decorate<IRepository<CarInteriorMaterial>, CachedRepository<CarInteriorMaterial>>();
        
        services.AddScoped<IRepository<CarInteriorColor>, Repository<CarInteriorColor>>();
        services.Decorate<IRepository<CarInteriorColor>, CachedRepository<CarInteriorColor>>();
        
        services.AddScoped<ICarBrandRepository, CarBrandRepository>();
        services.AddScoped<ICarModelRepository, CarModelRepository>();
        services.AddScoped<ICarGenerationRepository, CarGenerationRepository>();
        return services;
    }
}