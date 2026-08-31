using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Reservations;

namespace Hotels.Domain.Entities.Room
{
    public class Room : BaseEntity<string>
    {
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        //Relations
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
