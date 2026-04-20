
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

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _context.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }





}