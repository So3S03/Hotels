using Hotels.Domain.Entities.BaseEntities;

namespace Hotels.Domain.Entities.Reservations
{
    public class Reservation : BaseEntity<string>
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ReservationStatus Status { get; set; }

        //Relations
        public virtual required Room.Room Room { get; set; }
        public required string RoomId { get; set; }
    }
}
