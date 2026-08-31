using Hotels.Domain.Entities.Room;
using Hotels.Infrastructure.Persistence.Data.Configs.BaseConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotels.Infrastructure.Persistence.Data.Configs.RoomConfigs
{
    public class RoomConfigurations : BaseEntityConfigurations<Room, string>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);
            builder.HasIndex(R => R.RoomNumber).IsUnique();
            builder.Property(R => R.RoomNumber).IsRequired();
            builder.Property(R => R.PricePerNight).HasColumnType("decimel(18, 3)").IsRequired();
            builder.Property(R => R.IsAvailable).IsRequired();
            builder.Property(R => R.RoomType)
                .HasConversion(
                    (rt) => rt.ToString(),
                    (rt) => (RoomType)Enum.Parse(typeof(RoomType), rt)
                )
                .IsRequired();
            builder.HasMany(R => R.Reservations)
                .WithOne(R => R.Room).HasForeignKey(R => R.RoomId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
