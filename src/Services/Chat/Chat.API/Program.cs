using System.Reflection;
using System.Text;
using Auth.Shared;
using Chat.API.Hubs;
using Chat.Core.Extensions;
using Chat.Core.Options;
using Chat.DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.jwt.json"), optional: false, reloadOnChange: true);

builder.Services.AddSignalR();

builder.Services.AddControllers();

var applicationAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(applicationAssembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllers();

app.Run();