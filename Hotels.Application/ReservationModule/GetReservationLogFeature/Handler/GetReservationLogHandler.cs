using Hotels.Application._Common;
using Hotels.Application.ReservationModule.GetReservationLogFeature.Query;
using Hotels.Application.Specifications.ReservationSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.LogsModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReservationModule.GetReservationLogFeature.Handler
{
    public class GetReservationLogHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetReservationLogQuery, GridsToReturnDto<LogToReturnDto>>
    {
        public async Task<GridsToReturnDto<LogToReturnDto>> Handle(GetReservationLogQuery request, CancellationToken cancellationToken)
        {
            var logRepo = _unitOfWork.GenerateRepo<AuditLog, string>();
            var logSpec = new ReservationsLogSpec(request);
            var logList = await logRepo.GetAllAsync(logSpec);
            var logListCountSpec = new ReservationsLogSpec(request, false);
            var logCount = await logRepo.GetQuery(logListCountSpec).CountAsync();
            var mappedList = _mapper.Map<ICollection<LogToReturnDto>>(logList);
            var obj = new GridsToReturnDto<LogToReturnDto>()
            {
                Data = mappedList,
                Total = logCount
            };
            return obj;
        }
    }
}
