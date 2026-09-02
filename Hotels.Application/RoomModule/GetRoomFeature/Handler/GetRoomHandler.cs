using Hotels.Application.RoomModule.GetRoomFeature.Query;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos.RoomModule;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;

namespace Hotels.Application.RoomModule.GetRoomFeature.Handler
{
    public class GetRoomHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetRoomQuery, RoomToReturnDto>
    {
        public async Task<RoomToReturnDto> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { RoomId: null or ""} => throw new BadRequest400Exception("Invalid RoomId"),
                _ => request
            };
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var spec = new RoomByIdSpec(request.RoomId, true);
            var room = await roomRepo.GetAsync(spec);
            if (room is null) throw new NotFound404Exception("Room not found");
            var mappedRoom = _mapper.Map<Room, RoomToReturnDto>(room);
            return mappedRoom;
        }
    }
}
