using System.Reflection.Metadata;
using GymManagement.Domain.Subscriptions;
using GymManagement.TestCommon.TestConstants;

namespace GymManagement.TestCommon;


public static class SubscriptionFactory
{

    public static Subscription CreateSubscription(SubscriptionType? subscriptionType = null, Guid? adminID = null, Guid? id = null)
    {

        return new Subscription(
            subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            adminID ?? Constants.Subscription.AdminId,
            id ?? Constants.Subscription.Id
        );
    }

}