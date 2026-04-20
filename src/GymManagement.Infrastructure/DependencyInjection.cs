using GymManagement.Application.Common;
using GymManagement.Infrastructure.Gyms;
using GymManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;



public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<GymManagementDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        serviceCollection.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        serviceCollection.AddScoped<IGymsRepository, GymsRepository>();
        serviceCollection.AddScoped<IUnitOfWork>(serviceCollection =>
            serviceCollection.GetRequiredService<GymManagementDbContext>()
        );


        return serviceCollection;
    }

}