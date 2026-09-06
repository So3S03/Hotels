namespace Hotels.Shared.Dtos.ReportModule
{
    public class RevenueToReturnDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TotalReservations { get; set; }
        public int TotalNights { get; set; }
        public decimal TotalRevenue { get; set; }
        public string RoomType { get; set; }
    }
}
