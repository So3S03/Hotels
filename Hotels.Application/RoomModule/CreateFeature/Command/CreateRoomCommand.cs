using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.RoomModule.CreateFeature.Command
{
    public class CreateRoomCommand : IRequest<ActionStatusDto>
    {
        public int RoomNumber { get; set; }
        public int RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
