using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common;


public interface ISubscriptionRepository
{

    public Task<Subscription?> GetByIdAsync(Guid id);

    public Task AddAsync(Subscription subscription);


}