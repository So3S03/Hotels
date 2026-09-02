using Hotels.Shared.Dtos.RoomModule;
using MediatR;

namespace Hotels.Application.RoomModule.GetRoomFeature.Query
{
    public class GetRoomQuery : IRequest<RoomToReturnDto>
    {
        public string RoomId { get; set; }
    }
}
