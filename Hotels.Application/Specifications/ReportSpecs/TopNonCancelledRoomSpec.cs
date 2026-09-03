using Hotels.Application.ReportModule.TopRoomsFeature.Query;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.ReportSpecs
{
    internal class TopNonCancelledRoomSpec : BaseSpecification<Room>
    {
        public TopNonCancelledRoomSpec(GetTopNonCancelledRoomsQuery query, bool isLoadRelation = true, bool isPagination = true): base(
               R => R.RoomType == query.Type
            )
        {
            if (isLoadRelation) addIncludes(R => R.Reservations.Where(R => R.Status != ReservationStatus.Cancelled));
            setOrderBy(R => R.Reservations.Count(), false);
            if(isPagination && (query.PageNum > 0 || query.PageSize > 0)) Pagination(query.PageNum > 0 ? query.PageNum : 1, query.PageSize > 0 ? query.PageSize : 5);
        }
    }
}
