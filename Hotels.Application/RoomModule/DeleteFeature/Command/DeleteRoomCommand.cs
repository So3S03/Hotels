using Hotels.Shared.Dtos._Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Hotels.Application.RoomModule.DeleteFeature.Command
{
    public class DeleteRoomCommand : IRequest<ActionStatusDto>
    {
        public string RoomId { get; set; }

        [JsonIgnore]
        public string? ConnectionId { get; set; }
    }
}
