using GymManagement.Domain.Subscriptions;

namespace GymManagement.TestCommon.TestConstants;

public partial class Constants
{

    public static class Subscription
    {

        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly Guid AdminId = Guid.NewGuid();

        public const int MaxGymsFreeTier = 1;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxSessionsFreeTier = 3;


    }

}