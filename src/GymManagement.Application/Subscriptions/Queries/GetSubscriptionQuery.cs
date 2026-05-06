using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement;


public record GetSubscriptionQuery(Guid Id) : IRequest<ErrorOr<Subscription>>;
