namespace Hotels.Shared.Dtos.RoomModule
{
    public class ModifyRoomDto
    {
        public string Id { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
