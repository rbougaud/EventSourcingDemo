using Domain.Abstractions.Repositories;
using Infrastructure.Abstractions.Stores;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EventSourcingContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")));

        services.AddDbContext<ReadModelContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")), ServiceLifetime.Transient);

        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IRepositoryStudentWriter, RepositoryStudentWriter>();
        services.AddTransient<IRepositoryStudentReader, RepositoryStudentReader>();

        return services;
    }
}
