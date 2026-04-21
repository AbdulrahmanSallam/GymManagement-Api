using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;

    public GetGymQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {

        if (!await _subscriptionRepository.ExistsAsync(request.SubscriptionId))
        {
            return Error.NotFound("Subscription not found");
        }

        if (await _gymsRepository.GetByIdAsync(request.GymId) is not Gym gym)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;
    }
}