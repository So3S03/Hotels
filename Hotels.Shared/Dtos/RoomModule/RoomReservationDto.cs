namespace Hotels.Shared.Dtos.RoomModule
{
    public class RoomReservationDto
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
