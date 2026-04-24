using GymManagement.Application.Gyms.Commands.CreateGym;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;



public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            configuration.AddBehavior<CreateGymCommandBehavior>();
        });
        return serviceCollection;
    }

}