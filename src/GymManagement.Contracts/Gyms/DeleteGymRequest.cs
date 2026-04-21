namespace GymManagement.Contracts.Gyms;


public record DeleteGymRequest(Guid SubscriptionId, Guid GymId);
