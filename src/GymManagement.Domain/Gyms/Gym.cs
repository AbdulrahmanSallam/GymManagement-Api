using ErrorOr;
using GymManagement.Domain.Rooms;
using Throw;

namespace GymManagement.Domain.Gyms;


public partial class Gym
{
    private readonly int _maxRooms;

    public Guid Id { get; }

    private readonly List<Guid> _roomIds = new();
    private readonly List<Guid> _trainerIds = new();

    public string Name { get; init; }

    public Guid SubscriptionId { get; init; }

    public Gym(string name, Guid subscriptionId, int maxrooms, Guid? id = null)
    {
        Name = name;
        SubscriptionId = subscriptionId;
        _maxRooms = maxrooms;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        _roomIds.Throw().IfContains(room.Id);

        if (_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscrpitonAllows;
        }

        _roomIds.Add(room.Id);

        return Result.Success;
    }

    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if (_trainerIds.Contains(trainerId))
        {
            return Error.Conflict(description: "Trainer already added to gym");
        }

        _trainerIds.Add(trainerId);

        return Result.Success;

    }

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    public void RemoveRoom(Guid roomId)
    {
        _roomIds.Remove(roomId);
    }

    private Gym()
    {

    }

}