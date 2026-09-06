using Hotels.Application._Common;
using Hotels.Domain.Entities.Reservations;
using Hotels.Shared.Dtos.ReservationModule;

namespace Hotels.Application.ReservationModule.GetAllReservationsFeature.Query
{
    public class GetAllReservationsQuery : CommonGridQuery<ReservationToReturnDto>
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? StartPriceRange { get; set; }
        public decimal? EndPriceRange { get; set; }
        public ReservationStatus? Status { get; set; }
        public string? GuestName { get; set; }
    }
}
