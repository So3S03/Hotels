namespace Hotels.Shared.Dtos.RoomModule
{
    public class RoomToReturnDto
    {
        public string Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<RoomReservationDto> Reservations { get; set; }
    }
}
