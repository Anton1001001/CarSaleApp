using System.Reflection;
using DotNetEnv;
using File.Core.Extensions;
using File.DataAccess.Extensions;
using File.GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpClient();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddGrpc();

var applicationAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.MapGrpcService<FileService>();

app.Run();