using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.RoomSpecs
{
    public class RoomByIdSpec : BaseSpecification<Room>
    {
        public RoomByIdSpec(string id): base(r => r.Id == id)
        {
            
        }
    }
}
