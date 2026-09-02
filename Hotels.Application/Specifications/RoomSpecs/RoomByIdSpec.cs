using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.Room;

namespace Hotels.Application.Specifications.RoomSpecs
{
    public class RoomByIdSpec : BaseSpecification<Room>
    {
        public RoomByIdSpec(string id, bool isLoadRelation = false): base(r => r.Id == id)
        {
            if(isLoadRelation)
            {
                addIncludes(r => r.Reservations);
            }
        }
    }
}
