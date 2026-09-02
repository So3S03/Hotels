using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.RoomModule.UpdateFeature.Command
{
    public class UpdateRoomCommand : IRequest<ActionStatusDto>
    {
        public string Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
