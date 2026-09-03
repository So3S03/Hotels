using Hotels.Application.ReportModule.OccupancyFeature.Query;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.ReportSpecs
{
    internal class RoomsOccupancySpec : BaseSpecification<Room>
    {
        public RoomsOccupancySpec(RoomsOccupancyQuery query, bool isRelationLoading = true, bool isPagination = true) : base()
        {
            if (isRelationLoading) addIncludes(R => R.Reservations.Where(RS => RS.CheckInDate < query.To && RS.CheckOutDate > query.From && RS.Status != ReservationStatus.Cancelled));
            if(isPagination && (query.PageNum > 0 || query.PageSize > 0)) Pagination(query.PageNum, query.PageSize);
        }
    }
}
