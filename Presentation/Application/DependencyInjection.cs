using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            services.AddValidatorsFromAssembly(assembly);
        }
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
