using ErrorOr;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers;

[Route("api/[controller]")]
public class SubscriptionsController : ApiController
{

    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
    {

        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem(statusCode: 500, detail: "Invalid subscription type.");
        }

        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match(
            subscription => CreatedAtAction(
                nameof(GetSubscription),
                new { subscriptionId = subscription.Id },
                new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.SubscriptionType)
                )
            ),
         Problem
        );
    }


    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription([FromRoute] GetSubscriptionRequest request)
    {
        var query = new GetSubscriptionQuery(request.SubscriptionId);

        var getSubscriptionResult = await _mediator.Send(query);

        return getSubscriptionResult.Match(
            subscription => Ok(new SubscriptionResponse(
                subscription.Id,
                ToDto(subscription.SubscriptionType))
            ),
            Problem
        );
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription([FromRoute] DeleteSubscriptionRequest request)
    {
        var command = new DeleteSubscriptionCommand(request.SubscriptionId);

        var deleteSubscriptionResult = await _mediator.Send(command);

        return deleteSubscriptionResult.Match(
            _ => NoContent(),
            Problem
        );
    }


    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
            nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
            nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }

}
