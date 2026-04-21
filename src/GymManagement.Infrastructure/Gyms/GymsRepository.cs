
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbcontext)
    {
        _dbContext = dbcontext;
    }


    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.AddAsync(gym);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Gyms.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }

    public async Task<List<Gym>> ListGymsBySubscription(Guid subscriptionId)
    {
        return await _dbContext.Gyms.Where(gym => gym.SubscriptionId == subscriptionId).ToListAsync();
    }

    public Task RemoveGymAsync(Gym gym)
    {
        _dbContext.Gyms.Remove(gym);
        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        _dbContext.RemoveRange(gyms);

        return Task.CompletedTask;
    }

    public Task UpdateGymAsync(Gym gym)
    {
        _dbContext.Update(gym);

        return Task.CompletedTask;
    }
}