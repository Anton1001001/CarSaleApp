using System.Reflection;
using Advert.Application.Extensions;
using Advert.GrpcService.Services;
using Advert.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddGrpc();

var app = builder.Build();


app.MapGrpcService<AdvertService>();

app.Run();