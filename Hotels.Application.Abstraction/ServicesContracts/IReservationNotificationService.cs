using Hotels.Shared.Dtos.ReservationModule;

namespace Hotels.Application.Abstraction.ServicesContracts
{
    public interface IReservationNotificationService
    {
        Task NotifyReservationCreation(CreateReservationDto reservationDto, string? excludeConnectionId);
        Task NotifyReservationCancellation(ReservationToReturnDto reservationDto, string? excludeConnectionId);
    }
}
