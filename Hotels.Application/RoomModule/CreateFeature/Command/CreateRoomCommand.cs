using Hotels.Shared.Dtos._Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Hotels.Application.RoomModule.CreateFeature.Command
{
    public class CreateRoomCommand : IRequest<ActionStatusDto>
    {
        public int RoomNumber { get; set; }
        public int RoomType { get; set; }
        public decimal PricePerNight { get; set; }

        [JsonIgnore]
        public string? ConnectionId { get; set; }
    }
}
