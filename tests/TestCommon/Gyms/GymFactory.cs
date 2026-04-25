using GymManagement.Domain.Gyms;
using GymManagement.TestCommon.TestConstants;

namespace GymManagement.TestCommon.Gyms;


public static class GymFactory
{

    public static Gym CreateGym(string name = Constants.Gym.Name, int maxrooms = Constants.Subscription.MaxRoomsFreeTier, Guid? id = null)
    {

        return new Gym(
            name,
            Constants.Subscription.Id,
            maxrooms,
            id ?? Constants.Gym.Id
        );
    }

}