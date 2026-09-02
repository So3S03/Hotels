using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Reservations;

namespace Hotels.Application.Specifications.ReservationSpecs
{
    internal class ReservationById :BaseSpecification<Reservation>
    {
        public ReservationById(string reservationId, bool isLoadRelation = false): base(R => R.Id == reservationId)
        {
            if (isLoadRelation) addIncludes(R => R.Room);
        }
    }
}
