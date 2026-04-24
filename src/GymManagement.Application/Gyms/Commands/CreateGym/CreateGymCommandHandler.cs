using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(IGymsRepository gymRepository, ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {

        var validator = new CreateGymCommandValidator();

        var validatonResult = await validator.ValidateAsync(command);

        if (!validatonResult.IsValid)
        {
            return validatonResult.Errors
            .Select(error => Error.Validation(error.PropertyName, error.ErrorMessage))
            .ToList();
        }



        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);
        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        var gym = new Gym(
            command.Name,
            command.SubscriptionId,
            subscription.GetMaxGyms()
        );

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        await _subscriptionRepository.UpdateAsync(subscription);
        await _gymRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}