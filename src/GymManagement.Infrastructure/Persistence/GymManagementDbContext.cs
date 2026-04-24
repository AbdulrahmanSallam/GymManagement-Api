using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.Common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence;



public class GymManagementDbContext : DbContext, IUnitOfWork
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Gym> Gyms { get; set; }



    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task CommitChangesAsync()
    {
        // get all domain eventns then store it for handle it later in middleware
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        AddDomainEventsToOfflineProcessingQueue(domainEvents);

        await base.SaveChangesAsync();
    }


    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        var domainEventsqueue = _httpContextAccessor.HttpContext!.Items
        .TryGetValue("DomainEvents", out var value) && value is Queue<IDomainEvent> existingDomainEvent
            ? existingDomainEvent
            : new Queue<IDomainEvent>();

        domainEvents.ForEach(domainEventsqueue.Enqueue);

        _httpContextAccessor.HttpContext!.Items["DomainEvents"] = domainEventsqueue;

    }


}