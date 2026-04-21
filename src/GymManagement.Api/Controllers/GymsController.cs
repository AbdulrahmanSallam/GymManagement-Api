using ErrorOr;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("api/subscriptions/{subscriptionId:guid}/[controller]")]
public class GymsController : ApiController
{
    private readonly ISender _mediator;

    public GymsController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await _mediator.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, gymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            Problem
        );

    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var query = new GetGymQuery(subscriptionId, gymId);

        var getGymResult = await _mediator.Send(query);

        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            Problem
        );
    }





}
