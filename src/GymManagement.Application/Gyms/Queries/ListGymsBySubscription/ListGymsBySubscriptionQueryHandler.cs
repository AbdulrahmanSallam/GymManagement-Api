using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public class ListGymsBySubscriptionQueryHandler
(IGymsRepository _gymsRepository,
ISubscriptionsRepository _subscriptionsRepository)
: IRequestHandler<ListGymsBySubscriptionQuery, ErrorOr<List<Gym>>>
{

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsBySubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscription = _subscriptionsRepository.ExistsAsync(request.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound("Subscription not found");
        }

        return await _gymsRepository.ListGymsBySubscription(request.SubscriptionId);
    }
}