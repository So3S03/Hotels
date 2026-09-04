using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Hotels.Application.ReservationModule.CreateReservationFeature.Command
{
    public class CreateReservationCommand : IRequest<ActionStatusDto>
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public required string RoomId { get; set; }

        [JsonIgnore]
        public string? ConnectionId { get; set; }
    }
}
