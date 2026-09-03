using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.LogsModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.RoomModule.GetRoomLogFeature.GetRoomLogHandler
{
    public class GetRoomLogHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetRoomLogQuery.GetRoomLogQuery, GridsToReturnDto<LogToReturnDto>>
    {
        public async Task<GridsToReturnDto<LogToReturnDto>> Handle(GetRoomLogQuery.GetRoomLogQuery request, CancellationToken cancellationToken)
        {
            var logRepo = _unitOfWork.GenerateRepo<AuditLog, string>();
            var roomSpec = new RoomsLogSpec(request);
            var logList = await logRepo.GetAllAsync(roomSpec);
            var logCountSpec = new RoomsLogSpec(request, false);
            var count = await logRepo.GetQuery(roomSpec).CountAsync();
            var mappedList = _mapper.Map<ICollection<LogToReturnDto>>(logList);
            var obj = new GridsToReturnDto<LogToReturnDto>()
            {
                Data = mappedList,
                Total = count
            };
            return obj;
        }
    }
}
