using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using MediatR;

namespace GymManagement.Application.Subscriptions.Events;

public class SubscriptionDeletedEventHandler : INotificationHandler<SubscriptionDeletedEvent>
{

    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionDeletedEventHandler(ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {

        var subscription = await _subscriptionRepository.GetByIdAsync(notification.SubscriptionId) ?? throw new InvalidOperationException();

        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();
    }
}