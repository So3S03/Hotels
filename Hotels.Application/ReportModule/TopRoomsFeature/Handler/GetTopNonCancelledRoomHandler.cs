using Hotels.Application.ReportModule.TopRoomsFeature.Query;
using Hotels.Application.Specifications.ReportSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReportModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReportModule.TopRoomsFeature.Handler
{
    public class GetTopNonCancelledRoomHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetTopNonCancelledRoomsQuery, GridsToReturnDto<TopNonCancelledRoomToReturnDto>>
    {
        public async Task<GridsToReturnDto<TopNonCancelledRoomToReturnDto>> Handle(GetTopNonCancelledRoomsQuery request, CancellationToken cancellationToken)
        {
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var roomSpec = new TopNonCancelledRoomSpec(request);
            var rooms = await roomRepo.GetAllAsync(roomSpec);
            var countSpec = new TopNonCancelledRoomSpec(request, true, false);
            var count = await roomRepo.GetQuery(countSpec).CountAsync();
            var mappedList = _mapper.Map<ICollection<TopNonCancelledRoomToReturnDto>>(rooms);
            var obj = new GridsToReturnDto<TopNonCancelledRoomToReturnDto>()
            {
                Data = mappedList,
                Total = count
            };
            return obj;
        }
    }
}
