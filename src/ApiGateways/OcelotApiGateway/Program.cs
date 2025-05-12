using Auth.Shared;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.jwt.json"), optional: false, reloadOnChange: true);

builder.Services.AddRequestTimeouts();
builder.Services.AddAuthorization();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot().AddPolly();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();
app.UseWebSockets();
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();