using Hotels.Shared.Dtos.RoomModule;

namespace Hotels.Application.Abstraction.ServicesContracts
{
    public interface IRoomNotificationService
    {
        Task NotifyRoomCreation(CreateRoomDto room, string? excludeConnectionId);
        Task NotifyRoomModification(ModifyRoomDto room, string? excludeConnectionId);
        Task NotifyRoomDeletion(RoomToReturnDto room, string? excludeConnectionId);
    }
}
