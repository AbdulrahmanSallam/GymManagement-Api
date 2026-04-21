using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common;


public interface IGymsRepository
{
    public Task AddGymAsync(Gym gym);
    public Task<Gym?> GetByIdAsync(Guid id);

    public Task<List<Gym>> ListGymsBySubscription(Guid subscriptionId);

    public Task RemoveGymAsync(Gym gym);
    public Task RemoveRangeAsync(List<Gym> gyms);

    public Task UpdateGymAsync(Gym gym);
}