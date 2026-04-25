using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using GymManagement.TestCommon;

namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionsTests
{

    [Fact]
    public void AddGym_ShouldFail_WhenAddMoreThanSubscriptionAllows()
    {

        // arrange 
        // create subscription
        var subscription = SubscriptionFactory.CreateSubscription();
        // create gyms list with one more than the allowed
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
                .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
                .ToList();


        // Act 
        // add gyms and store its result 

        var addGymsResult = gyms.ConvertAll(subscription.AddGym).ToList();

        // assert

        var allButLastGymResults = addGymsResult[..^1];
        allButLastGymResults.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

        var lastGymResult = addGymsResult.Last();
        lastGymResult.IsError.Should().BeTrue();
        lastGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);
    }

}