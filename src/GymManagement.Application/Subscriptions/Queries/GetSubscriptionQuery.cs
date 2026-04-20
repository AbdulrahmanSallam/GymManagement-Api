using ErrorOr;
using GymManagement.Application.Common;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement;


public record GetSubscriptionQuery(Guid Id) : IRequest<ErrorOr<Subscription>>;


public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        this._subscriptionRepository = subscriptionRepository;
    }
    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.Id);

        return subscription is not null
        ? subscription
        : Error.NotFound("Subscription Not Found");
    }
}