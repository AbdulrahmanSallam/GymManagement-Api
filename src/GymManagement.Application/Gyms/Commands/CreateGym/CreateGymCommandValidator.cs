using FluentValidation;

namespace GymManagement.Application.Gyms.Commands.CreateGym;


public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{

    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(18);
    }

}

