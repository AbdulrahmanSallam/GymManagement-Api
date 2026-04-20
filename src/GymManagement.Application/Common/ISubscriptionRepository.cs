using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common;


public interface ISubscriptionRepository
{

    Task<Subscription?> GetByIdAsync(Guid id);

    Task AddAsync(Subscription subscription);

    Task UpdateAsync(Subscription subscription);

}