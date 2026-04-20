using ErrorOr;

namespace GymManagement.Domain.Gyms;


public partial class Gym
{
    public static class GymErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanSubscrpitonAllows = Error.Validation(
            code: "CannotHaveMoreRoomsThanSubscrpitonAllows",
            description: "A gym cannot have more rooms than the subscription allows"
        );
    }

}