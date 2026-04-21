using ErrorOr;
using GymManagement.Domain.Gyms;
using Throw;

namespace GymManagement.Domain.Subscriptions;


public class Subscription
{
    public Guid AdminId { get; }

    public Guid Id { get; private set; }

    public SubscriptionType SubscriptionType { get; private set; }

    private readonly int _maxGyms;

    private readonly List<Guid> _gymIds = new();


    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();

        _maxGyms = GetMaxGyms();
    }


    public ErrorOr<Success> AddGym(Gym gym)
    {
        _gymIds.Throw().IfContains(gym.Id);

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid GymId)
    {
        return _gymIds.Contains(GymId);
    }

    public void RemoveGym(Guid gymId)
    {
        _gymIds.Throw().IfNotContains(gymId);

        _gymIds.Remove(gymId);
    }

    private Subscription()
    {

    }


}