using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingApi.Application.Common.Interfaces;
using ParkingApi.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    private const string InMemoryDbName = "ParkingDb";

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        var inMemoryServiceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseInMemoryDatabase(InMemoryDbName);
            options.UseInternalServiceProvider(inMemoryServiceProvider);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddControllers();
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
