using System.Reflection;
using File.Core.Extensions;
using File.DataAccess.Extensions;
using File.GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddGrpc();

var applicationAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.MapGrpcService<FileService>();

app.Run();