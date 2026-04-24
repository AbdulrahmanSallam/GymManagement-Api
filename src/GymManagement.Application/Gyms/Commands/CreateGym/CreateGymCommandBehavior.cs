using ErrorOr;
using FluentValidation;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;


public class CreateGymCommandBehavior : IPipelineBehavior<CreateGymCommand, ErrorOr<Gym>>
{
    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, RequestHandlerDelegate<ErrorOr<Gym>> next, CancellationToken cancellationToken)
    {

        var validator = new CreateGymCommandValidator();

        var validatonResult = await validator.ValidateAsync(request);

        if (!validatonResult.IsValid)
        {
            return validatonResult.Errors
            .Select(error => Error.Validation(error.PropertyName, error.ErrorMessage))
            .ToList();
        }

        return await next();
    }
}

