namespace Car.Application.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        return services;
    }
}