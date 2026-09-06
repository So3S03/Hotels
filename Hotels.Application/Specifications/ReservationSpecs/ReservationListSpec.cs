using Hotels.Application.ReservationModule.GetAllReservationsFeature.Query;
using Hotels.Application.Specifications._Common;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace Hotels.Application.Specifications.ReservationSpecs
{
    internal class ReservationListSpec : BaseSpecification<Reservation>
    {
        public ReservationListSpec(GetAllReservationsQuery query, bool isPagination = true) : base(
                CritriaCreator.CreateCriteria<Reservation>(
                        GetDateRange(query.StartDate, query.EndDate)!,
                        GetPriceRange(query.StartPriceRange, query.EndPriceRange)!,
                        GetStatus(query.Status)!,
                        GetGuestName(query.GuestName)!
                    )
            )
        {
            if ((query.PageNum > 0 || query.PageSize > 0) && isPagination) Pagination(query.PageNum, query.PageSize);
            addIncludes(r => r.Room);
            setOrderBy(R => R.CheckInDate, false);
        }

        private static Expression<Func<Reservation, bool>>? GetDateRange(DateOnly? startDate, DateOnly? endDate)
        {
            if(startDate is null && endDate is null) return null;
            if (startDate is null && endDate is not null) return R => R.CheckOutDate <= endDate;
            if (startDate is not null && endDate is null) return R => R.CheckInDate >= startDate;
            return R => R.CheckInDate >= endDate && R.CheckOutDate <= startDate;
        }

        private static Expression<Func<Reservation, bool>>? GetPriceRange(decimal? startPrice, decimal? endPrice)
        {
            if(startPrice is null && endPrice is null) return null;
            if (startPrice is null && endPrice is not null) return R => R.TotalAmount <= endPrice;
            if (startPrice is not null && endPrice is null) return R => R.TotalAmount >= startPrice;
            return R => R.TotalAmount >= endPrice && R.TotalAmount <= startPrice;
        }

        private static Expression<Func<Reservation, bool>>? GetStatus(ReservationStatus? status)
        {
            if(status is null) return null;
            return R => R.Status == status;
        }

        private static Expression<Func<Reservation, bool>>? GetGuestName(string? name)
        {
            if(string.IsNullOrEmpty(name)) return null;
            return R => R.GuestName.ToLower().Contains(name.ToLower());
        }
    }
}
