using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IAdminsRepository _adminsRepository;

    private readonly IUnitOfWork _unitOfWork;


    public DeleteSubscriptionCommandHandler(ISubscriptionsRepository subscriptionRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork, IAdminsRepository adminsRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var admin = await _adminsRepository.GetByIdAsync(subscription.AdminId);
        if (admin is null)
        {
            return Error.Unexpected(description: "Admin not found");
        }

        admin.DeleteSubscription(command.SubscriptionId);

        var gymsToDelete = await _gymsRepository.ListGymsBySubscription(command.SubscriptionId);

        await _adminsRepository.UpdateAsync(admin);
        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _gymsRepository.RemoveRangeAsync(gymsToDelete);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}