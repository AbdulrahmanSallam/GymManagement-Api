using System.Reflection.Metadata;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.TestCommon.TestConstants;

namespace GymManagement.TestCommon.Gyms;


public static class GymCommandFactory
{


    public static CreateGymCommand CreateCreateGymCommand(string? name = null, Guid? subscriptionId = null)
    {
        return new CreateGymCommand(
            Constants.Gym.Name,
            Constants.Subscription.Id
        );
    }
}