using Hotels.Application._Common;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos.AuthModule;
using Hotels.Shared.Dtos.RoomModule;

namespace Hotels.Application.RoomModule.GetAllRoomsFeature.Query
{
    public class GetAllRoomsQuery : CommonGridQuery<RoomToReturnDto>
    {
        public RoomType? Type { get; set; }
        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }
        public bool? isAvailable { get; set; }
    }
}
