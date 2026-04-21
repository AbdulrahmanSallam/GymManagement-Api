using GymManagement.Application.Rooms.Commands.CreateRoom;
using GymManagement.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("api/gyms/{gymId:guid}/rooms")]
public class RoomsController : ApiController
{
    private readonly ISender _mediator;

    public RoomsController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> CreateRoom(
          CreateRoomRequest request,
          Guid gymId)
    {
        var command = new CreateRoomCommand(
            gymId,
            request.Name);

        var createRoomResult = await _mediator.Send(command);

        return createRoomResult.Match(
            room => Created(
                $"rooms/{room.Id}", // todo: add host
                new RoomResponse(room.Id, room.Name)),
            _ => Problem());
    }







}

