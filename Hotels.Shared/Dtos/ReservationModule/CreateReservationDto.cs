namespace Hotels.Shared.Dtos.ReservationModule
{
    public class CreateReservationDto
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string Status { get; set; }
        public required string RoomId { get; set; }
        public required string RoomNumber { get; set; }
    }
}
