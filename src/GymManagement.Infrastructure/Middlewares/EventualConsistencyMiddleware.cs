using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Infrastructure.Middlewares;


public class EventualConsistencyMiddleware(RequestDelegate next)
{

    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();

        context.Response.OnCompleted(async () =>
        {

            try
            {
                if (context.Items.TryGetValue("DomainEvents", out var value) && value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }

                }


                await transaction.CommitAsync();
            }
            catch
            {
                // notify the client that even though they got a good response, the changes didn't take a place due to unexpected error
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }

}