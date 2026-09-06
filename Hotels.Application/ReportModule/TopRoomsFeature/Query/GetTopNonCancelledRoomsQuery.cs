using Hotels.Application._Common;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos.ReportModule;

namespace Hotels.Application.ReportModule.TopRoomsFeature.Query
{
    public class GetTopNonCancelledRoomsQuery : CommonGridQuery<TopNonCancelledRoomToReturnDto>
    {
        public RoomType? Type { get; set; }
    }
}
