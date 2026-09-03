using Hotels.Application.ReportModule.RevenueFeature.Query;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.ReportSpecs
{
    internal class RoomsRevenueInDateRangeSpec : BaseSpecification<Reservation>
    {
        public RoomsRevenueInDateRangeSpec(GetRevenueQuery query) : base(
                RS => RS.CheckInDate >= query.From && RS.CheckOutDate <= query.To && RS.Status != ReservationStatus.Cancelled
            )
        {
            addIncludes(R => R.Room);
        }
    }
}
