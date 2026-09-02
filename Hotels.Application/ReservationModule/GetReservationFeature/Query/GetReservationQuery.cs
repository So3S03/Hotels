using Hotels.Shared.Dtos.ReservationModule;
using MediatR;

namespace Hotels.Application.ReservationModule.GetReservationFeature.Query
{
    public class GetReservationQuery : IRequest<ReservationToReturnDto>
    {
        public string ReservationId { get; set; }
    }
}
