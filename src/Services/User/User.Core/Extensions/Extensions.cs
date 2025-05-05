using System.Reflection;
using Auth.Shared;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using User.Core.Abstractions;
using User.Core.CQRS.Commands.RegisterByEmail;
using User.Core.CQRS.Commands.SignInByEmail;
using User.Core.Options;
using User.Core.Services;

namespace User.Core.Extensions;

public static class Extensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>( o =>
        {
            o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
        } );
        services.AddTransient<IResend, ResendClient>();
        
        services.AddScoped<IUserEmailService, UserEmailService>();
        services.AddScoped<IJwtService, JwtService>();
        
        services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddTransient<IPipelineBehavior<RegisterByEmailCommand, Result<RegisterByEmailResponse>>,
            RegisterByEmailValidationBehavior>();
        services.AddTransient<IPipelineBehavior<SignInByEmailCommand, Result<SignInByEmailResponse>>,
            SignInByEmailValidationBehavior>();
        
        services.AddValidatorsFromAssemblyContaining<RegisterByEmailCommandValidator>();


        return services;
    }
}