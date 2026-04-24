using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins;
using GymManagement.Infrastructure.Gyms;
using GymManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;



public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GymManagementDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IUnitOfWork>(serviceCollection =>
            serviceCollection.GetRequiredService<GymManagementDbContext>()
        );

        return services;
    }

}