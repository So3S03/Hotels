using Hotels.Domain.Entities.Reservations;
using Hotels.Infrastructure.Persistence.Data.Configs.BaseConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotels.Infrastructure.Persistence.Data.Configs.ReservationConfigs
{
    public class ReservationConfigurations : BaseEntityConfigurations<Reservation, string>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);
            builder.Property(R => R.GuestName).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(R => R.CheckInDate).HasColumnType("date").IsRequired();
            builder.Property(R => R.CheckOutDate).HasColumnType("date").IsRequired();
            builder.Property(R => R.TotalAmount).HasColumnType("decimal(18, 3)").IsRequired();
            builder.Property(R => R.Status).HasConversion(
                (s) => s.ToString(),
                (s) => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), s)
                ).IsRequired();
            builder.HasOne(R => R.Room).WithMany(R => R.Reservations)
                .HasForeignKey(R => R.RoomId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
