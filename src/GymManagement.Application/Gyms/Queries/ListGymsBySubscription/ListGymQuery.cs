using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public record ListGymsBySubscriptionQuery(Guid SubscriptionId)
: IRequest<ErrorOr<List<Gym>>>;
