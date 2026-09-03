namespace Hotels.Shared.Dtos.ReportModule
{
    public class TopNonCancelledRoomToReturnDto
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int ReservationCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
