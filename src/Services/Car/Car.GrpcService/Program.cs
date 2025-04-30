using System.Reflection;
using Car.Application.Extensions;
using Car.GrpcService.Services;
using Car.Infrastructure.Extensions;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
var applicationAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<CarCatalogService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();