namespace GymManagement.Domain.Common;



public class Entity
{

    public Guid Id { get; init; }

    protected readonly List<IDomainEvent> _domainEvents = [];


    public Entity(Guid id) => Id = id;


    private Entity() { }


}