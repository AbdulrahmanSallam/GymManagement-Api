using GymManagement.Application.Common;
using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly GymManagementDbContext _context;

    public SubscriptionRepository(GymManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == id);
    }


    public Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);

        return Task.CompletedTask;
    }



}