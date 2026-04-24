namespace GymManagement.Domain.Common;



public class Entity
{

    public Guid Id { get; init; }

    protected readonly List<IDomainEvent> _domainEvents = [];


    public Entity(Guid id) => Id = id;


    protected Entity() { }


    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();

        _domainEvents.Clear();

        return copy;
    }


}