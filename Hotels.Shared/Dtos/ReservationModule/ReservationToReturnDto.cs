namespace Hotels.Shared.Dtos.ReservationModule
{
    public class ReservationToReturnDto
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; }

        //Relations
        public string RoomNumber { get; set; }
        public required string RoomId { get; set; }
    }
}
