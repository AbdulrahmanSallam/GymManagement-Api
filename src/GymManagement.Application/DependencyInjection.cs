using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;



public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        return serviceCollection;
    }

}