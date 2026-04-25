using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gyms;
using GymManagement.TestCommon.Gyms;
using MediatR;
using NSubstitute;

namespace GymManagement.Application.UnitTests.Common.Behaviors;


public class ValidationBehaviorTests
{

    private readonly ValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _validationBehavior;
    private readonly IValidator<CreateGymCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehavior;


    public ValidationBehaviorTests()
    {


        // create nextBehavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();

        // create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();


        // create validationBehavior (mock)
        _validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(_mockValidator);

    }

    [Fact]
    public async Task InvokeBehavior_ShouldInvokeNextBehavior_WhenValidatorResultIsValid()
    {
        // Arrange 
        var CreateGymRequest = GymCommandFactory.CreateCreateGymCommand();

        var gym = GymFactory.CreateGym();

        _mockValidator
            .ValidateAsync(CreateGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(gym);

        // Act
        var invokeResult = await _validationBehavior.Handle(CreateGymRequest, _mockNextBehavior, default);


        //Assert
        invokeResult.IsError.Should().BeFalse();
        invokeResult.Value.Should().BeEquivalentTo(gym);
    }


    [Fact]
    public async Task InvokeBehavior_ShouldReturnErrors_WhenValidatorResultIsInvalid()
    {
        // Arrange 
        var CreateGymRequest = GymCommandFactory.CreateCreateGymCommand();

        List<ValidationFailure> validationFailure = [new(propertyName: "foo", errorMessage: "bad foo")];
        _mockValidator
            .ValidateAsync(CreateGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailure));


        // Act
        var invokeResult = await _validationBehavior.Handle(CreateGymRequest, _mockNextBehavior, default);


        //Assert
        invokeResult.IsError.Should().BeTrue();
        invokeResult.FirstError.Code.Should().Be("foo");
        invokeResult.FirstError.Description.Should().Be("bad foo");
    }

}