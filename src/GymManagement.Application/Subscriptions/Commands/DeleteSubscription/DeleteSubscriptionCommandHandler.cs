using ErrorOr;
using GymManagement.Application.Common;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymsRepository _gymsRepository;

    private readonly IUnitOfWork _unitOfWork;


    public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gymsToDelete = await _gymsRepository.ListGymsBySubscription(request.SubscriptionId);

        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _gymsRepository.RemoveRangeAsync(gymsToDelete);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}