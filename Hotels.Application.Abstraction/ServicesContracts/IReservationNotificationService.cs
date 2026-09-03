using Hotels.Shared.Dtos.ReservationModule;

namespace Hotels.Application.Abstraction.ServicesContracts
{
    public interface IReservationNotificationService
    {
        Task NotifyReservationCreation(CreateReservationDto reservationDto);
        Task NotifyReservationCancellation(ReservationToReturnDto reservationDto);
    }
}
