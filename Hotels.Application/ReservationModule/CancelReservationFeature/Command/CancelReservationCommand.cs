using Hotels.Shared.Dtos._Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command
{
    public class CancelReservationCommand : IRequest<ActionStatusDto>
    {
        public string ReservationId { get; set; }

        [JsonIgnore]
        public string? ConnectionId { get; set; }
    }
}
