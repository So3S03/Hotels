using Hotels.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotels.Infrastructure.Persistence.Data.Configs.IdentityConfigs
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(U => U.FullName).HasColumnType("nvarchar(max)").IsRequired();
            builder.HasIndex(U => U.Email).IsUnique();
            builder.Property(U => U.Email).IsRequired();
            builder.Property(U => U.isActive).IsRequired();
        }
    }
}
