namespace Hotels.Shared.Dtos.ReportModule
{
    public class OccupancyRoomsToReturnDto
    {
        public int RoomNumber { get; set; }
        public int TotalReservedNights { get; set; }
        public int TotalPeriodNights { get; set; }
        public decimal OccupancyPercentage { get; set; }
    }
}
