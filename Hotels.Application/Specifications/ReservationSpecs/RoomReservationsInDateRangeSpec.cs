using Hotels.Application.Specifications._Common;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;
using System.Linq.Expressions;

namespace Hotels.Application.Specifications.ReservationSpecs
{
    internal class RoomReservationsInDateRangeSpec : BaseSpecification<Reservation>
    {
        public RoomReservationsInDateRangeSpec(string RoomId, DateOnly startDate, DateOnly endDate) : base(
                CritriaCreator.CreateCriteria<Reservation>(
                        RoomCriteria(RoomId)!,
                        DateRangeCriteria(startDate, endDate)!,
                        StatusCriteria()!
                    )
            )
        {
            
        }

        private static Expression<Func<Reservation, bool>>? RoomCriteria(string? roomId)
        {
            if (string.IsNullOrEmpty(roomId)) return null;
            return R => R.RoomId == roomId;
        }

        private static Expression<Func<Reservation, bool>>? DateRangeCriteria(DateOnly? startDate, DateOnly? endDate)
        {
            if (startDate is null || endDate is null) return null;
            return R => R.CheckInDate < endDate && R.CheckOutDate > startDate;
        }

        private static Expression<Func<Reservation, bool>>? StatusCriteria()
        {
            return R => R.Status != ReservationStatus.Cancelled;
        }
    }
}
