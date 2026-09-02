using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using MediatR;

namespace Hotels.Application.ReservationModule.CreateReservationFeature.Command
{
    public class CreateReservationCommand : IRequest<ActionStatusDto>
    {
        public required string GuestName { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public required string RoomId { get; set; }
    }
}
