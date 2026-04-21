namespace GymManagement.Contracts.Gyms;


public record CreateGymRequest(string Name, Guid SubscriptionId);
