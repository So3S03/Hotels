using Hotels.Application.RoomModule.GetAllRoomsFeature.Query;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.RoomModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.RoomModule.GetAllRoomsFeature.Handler
{
    public class GetAllRoomsHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllRoomsQuery, GridsToReturnDto<RoomToReturnDto>>
    {
        public async Task<GridsToReturnDto<RoomToReturnDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var spec = new RoomsListSpec(request);
            var roomList = await roomRepo.GetAllAsync(spec);
            var countSpec = new RoomsListSpec(request, false);
            var count = await roomRepo.GetQuery(countSpec).CountAsync();
            var mappedList = _mapper.Map<ICollection<RoomToReturnDto>>(roomList);
            var Obj = new GridsToReturnDto<RoomToReturnDto>()
            {
                Data = mappedList,
                Total = count
            };
            return Obj;
        }
    }
}
