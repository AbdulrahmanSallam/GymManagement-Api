using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Subscriptions;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property("_adminId").HasColumnName("AdminId");

        builder.Property("_maxGyms").HasColumnName("MaxGym");

        builder.Property(
            s => s.SubscriptionType).HasConversion(subscriptionType => subscriptionType.Name,
            value => SubscriptionType.FromName(value)
        );

        builder.Property<List<Guid>>("_gymIds")
         .HasColumnName("GymIds")
         .HasListOfIdsConverter();

    }
}