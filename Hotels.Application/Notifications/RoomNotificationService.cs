using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.Hubs;
using Hotels.Shared.Dtos.RoomModule;
using Microsoft.AspNetCore.SignalR;

namespace Hotels.Application.Notifications
{
    public class RoomNotificationService(IHubContext<NotificationHub> _hubContext) : IRoomNotificationService
    {
        public async Task NotifyRoomCreation(CreateRoomDto room, string? excludeConnectionId)
            => await GetTargetedClinets(excludeConnectionId).SendAsync("RoomCreation", room);

        public async Task NotifyRoomDeletion(RoomToReturnDto room, string? excludeConnectionId)
            => await GetTargetedClinets(excludeConnectionId).SendAsync("RoomDeletion", room);

        public async Task NotifyRoomModification(ModifyRoomDto room, string? excludeConnectionId)
            => await GetTargetedClinets(excludeConnectionId).SendAsync("RoomModification", room);

        private IClientProxy GetTargetedClinets(string? connectionId)
            => string.IsNullOrEmpty(connectionId) ? _hubContext.Clients.All : _hubContext.Clients.AllExcept(connectionId);
    }
}
