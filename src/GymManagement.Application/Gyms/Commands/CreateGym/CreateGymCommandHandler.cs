using ErrorOr;
using GymManagement.Application.Common;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(IGymsRepository gymRepository, ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {

        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gym = new Gym(
            request.Name,
            request.SubscriptionId,
            subscription.GetMaxGyms()
        );

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        await _gymRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}