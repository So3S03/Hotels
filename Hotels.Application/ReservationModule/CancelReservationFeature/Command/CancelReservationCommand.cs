using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command
{
    public class CancelReservationCommand : IRequest<ActionStatusDto>
    {
        public string ReservationId { get; set; }
    }
}
