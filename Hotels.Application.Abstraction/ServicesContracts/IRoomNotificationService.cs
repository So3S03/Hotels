using Hotels.Shared.Dtos.RoomModule;

namespace Hotels.Application.Abstraction.ServicesContracts
{
    public interface IRoomNotificationService
    {
        Task NotifyRoomCreation(CreateRoomDto room);
        Task NotifyRoomModification(ModifyRoomDto room);
        Task NotifyRoomDeletion(RoomToReturnDto room);
    }
}
