using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common;


public interface IGymsRepository
{
    public Task AddGymAsync(Gym gym);
    public Task<Gym?> GetByIdAsync(Guid id);
}