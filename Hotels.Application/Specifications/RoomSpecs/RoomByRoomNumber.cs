using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.RoomSpecs
{
    internal class RoomByRoomNumber : BaseSpecification<Room>
    {
        public RoomByRoomNumber(int RoomNumber): base(r => r.RoomNumber == RoomNumber)
        {
            
        }
    }
}
