using ErrorOr;
using GymManagement.Application.Gyms.Commands.AddTrainer;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Commands.UpdateGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
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


    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
    {
        var command = new DeleteGymCommand(subscriptionId, gymId);

        var DeleteGymResult = await _mediator.Send(command);

        return DeleteGymResult.Match(
            _ => NoContent(),
            Problem
        );
    }


    [HttpGet]
    public async Task<IActionResult> ListGymsBySubscription(Guid subscriptionId)
    {
        var query = new ListGymsBySubscriptionQuery(subscriptionId);

        var listGymsBySubscriptionResult = await _mediator.Send(query);

        return listGymsBySubscriptionResult.Match(
        gyms => Ok(gyms.Select(gym => new GymResponse(gym.Id, gym.Name))),
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


    [HttpPost("{gymId:guid}")]
    public async Task<IActionResult> UpdateGym(Guid subscriptionId, Guid gymId, [FromBody] UpdateGymRequest request)
    {
        var command = new UpdateGymCommand(subscriptionId, gymId, request.Name);

        var UpdateGymResult = await _mediator.Send(command);

        return UpdateGymResult.Match(
              gym => Ok(new GymResponse(gym.Id, gym.Name)),
              Problem
          );
    }


    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
    {
        var command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        var addTrainerResult = await _mediator.Send(command);

        return addTrainerResult.Match(
            success => Ok(),
            Problem);
    }

}

