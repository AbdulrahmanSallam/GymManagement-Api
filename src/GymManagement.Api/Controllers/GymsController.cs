using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("api/subscriptions/{subscriptionId:guid}/[controller]")]
[ApiController]
public class GymsController : ControllerBase
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
          gym => Ok(new GymResponse(gym.Id, gym.Name)),
          error => Problem()
        );

    }




}
