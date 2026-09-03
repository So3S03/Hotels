namespace Hotels.Shared.Dtos.RoomModule
{
    public class CreateRoomDto
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
