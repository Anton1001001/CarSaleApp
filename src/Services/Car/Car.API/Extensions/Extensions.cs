namespace Car.API.Extensions;
public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("CarInfoDb");
        var serverVersion = ServerVersion.Parse(builder.Configuration.GetSection("DatabaseOptions:ServerVersion").Value);
        builder.Services.AddDbContext<CarInfoDbContext>(options => options.UseMySql(connectionString, serverVersion));
        builder.Services.AddMigration<CarInfoDbContext, CarInfoDbContextSeed>();
    }
}