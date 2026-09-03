using Hotels.Application._Common;
using Hotels.Shared.Dtos.LogsModule;

namespace Hotels.Application.RoomModule.GetRoomLogFeature.GetRoomLogQuery
{
    public class GetRoomLogQuery : CommonGridQuery<LogToReturnDto>
    {
        public string RoomId { get; set; }
    }
}
