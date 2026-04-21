namespace GymManagement.Contracts.Gyms;


public record GetGymRequest(Guid SubscriptionId, Guid GymId);
