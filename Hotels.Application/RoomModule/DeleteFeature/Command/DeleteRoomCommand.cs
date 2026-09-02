using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.RoomModule.DeleteFeature.Command
{
    public class DeleteRoomCommand : IRequest<ActionStatusDto>
    {
        public string RoomId { get; set; }
    }
}
