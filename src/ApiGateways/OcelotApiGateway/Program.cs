using System.Text;
using Auth.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .WithOrigins("http://localhost:4200", "http://localhost:57330") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.jwt.json"), optional: false, reloadOnChange: true);


builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseRouting();
app.UseWebSockets();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();