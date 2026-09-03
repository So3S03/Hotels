namespace Hotels.Shared.Dtos.ReportModule
{
    public class RevenueToReturnDto
    {
        public DateOnly StartDate { get; set; } //Will Be Range Came From User Not DataBase
        public DateOnly EndDate { get; set; } //Will Be Range Came From User Not DataBase
        public int TotalReservations { get; set; }
        public int TotalNights { get; set; }
        public decimal TotalRevenue { get; set; }
        public string RoomType { get; set; }
    }
}
