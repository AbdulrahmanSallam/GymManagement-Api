using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.UpdateGym;


public record UpdateGymCommand(Guid SubscriptionId, Guid GymId, string Name) : IRequest<ErrorOr<Gym>>;


public class UpdateGymCommandHandler(IGymsRepository _gymsRepository, ISubscriptionsRepository _subscriptionsRepository, IUnitOfWork _unitOfWork) : IRequestHandler<UpdateGymCommand, ErrorOr<Gym>>
{


    public async Task<ErrorOr<Gym>> Handle(UpdateGymCommand request, CancellationToken cancellationToken)
    {
        var IsSubscriptionExists = await _subscriptionsRepository.ExistsAsync(request.SubscriptionId);
        if (!IsSubscriptionExists)
        {
            return Error.NotFound(description: "Subsctipiton not found");
        }

        var gym = await _gymsRepository.GetByIdAsync(request.GymId);
        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        gym.UpdateGym(request.Name);

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}