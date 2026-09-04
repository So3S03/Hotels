using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.Hubs;
using Hotels.Shared.Dtos.ReservationModule;
using Microsoft.AspNetCore.SignalR;

namespace Hotels.Application.Notifications
{
    public class ReservationNotificationService(IHubContext<NotificationHub> _hubContext) : IReservationNotificationService
    {
        public async Task NotifyReservationCancellation(ReservationToReturnDto reservationDto, string? excludeConnectionId)
            => await GetTargetedClinets(excludeConnectionId).SendAsync("ReservationCancellation", reservationDto);

        public async Task NotifyReservationCreation(CreateReservationDto reservationDto, string? excludeConnectionId)
            => await GetTargetedClinets(excludeConnectionId).SendAsync("ReservationCreation", reservationDto);

        private IClientProxy GetTargetedClinets(string? connectionId)
            => string.IsNullOrEmpty(connectionId) ? _hubContext.Clients.All : _hubContext.Clients.AllExcept(connectionId);
    }
}
