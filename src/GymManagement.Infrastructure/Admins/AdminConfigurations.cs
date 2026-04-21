using GymManagement.Domain.Admins;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Admins;


public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasData(new Admin(
            userId: Guid.NewGuid(),
            id: Guid.Parse("2150e333-8fdc-42a3-9474-1a3956d46de8")
        ));
    }
}