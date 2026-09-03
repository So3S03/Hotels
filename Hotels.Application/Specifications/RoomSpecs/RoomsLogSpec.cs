using Hotels.Application.RoomModule.GetRoomLogFeature.GetRoomLogQuery;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.RoomSpecs
{
    public class RoomsLogSpec : BaseSpecification<AuditLog>
    {
        public RoomsLogSpec(GetRoomLogQuery query, bool isPagination = true): base(
                L => L.EntityId == query.RoomId && L.EntityName == typeof(Room).Name
            )
        {
            if ((query.PageNum > 0 || query.PageSize > 0) && isPagination) Pagination(query.PageNum > 0 ? query.PageNum : 1, query.PageSize > 0 ? query.PageSize : 1);
        }
    }
}
