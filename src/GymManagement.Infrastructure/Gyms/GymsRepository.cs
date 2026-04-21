
using GymManagement.Application.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _context;

    public GymsRepository(GymManagementDbContext context)
    {
        _context = context;
    }


    public async Task AddGymAsync(Gym gym)
    {
        await _context.AddAsync(gym);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _context.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }

    public async Task<List<Gym>> ListGymsBySubscription(Guid subscriptionId)
    {
        return await _context.Gyms.Where(gym => gym.SubscriptionId == subscriptionId).ToListAsync();
    }

    public Task RemoveGymAsync(Gym gym)
    {
        _context.Gyms.Remove(gym);
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        _context.RemoveRange(gyms);

        return Task.CompletedTask;
    }
}