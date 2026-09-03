using Hotels.Application.ReportModule.OccupancyFeature.Query;
using Hotels.Application.Specifications.ReportSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReportModule;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReportModule.OccupancyFeature.Handler
{
    public class RoomsOccupancyHandler(IUnitOfWork _unitOfWork) : IRequestHandler<RoomsOccupancyQuery, GridsToReturnDto<OccupancyRoomsToReturnDto>>
    {
        public async Task<GridsToReturnDto<OccupancyRoomsToReturnDto>> Handle(RoomsOccupancyQuery request, CancellationToken cancellationToken)
        {
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var roomSpec = new RoomsOccupancySpec(request);
            var totalNightsOnSelectedPeriod = request.To.DayNumber - request.From.DayNumber;
            var list = await roomRepo.GetQuery(roomSpec).GroupBy(R => R.RoomNumber).Select(E => new OccupancyRoomsToReturnDto()
            {
                RoomNumber = E.Key,
                TotalPeriodNights = totalNightsOnSelectedPeriod,
                TotalReservedNights = E.SelectMany(R => R.Reservations).Sum(RS =>
                    (RS.CheckOutDate < request.To ? RS.CheckOutDate : request.To).DayNumber
                    -
                    (RS.CheckInDate > request.From ? RS.CheckInDate : request.From).DayNumber
                    ),
                OccupancyPercentage = totalNightsOnSelectedPeriod > 0
                    ? Math.Round(
                        (decimal)(E.SelectMany(R => R.Reservations).Sum(RS =>
                        (RS.CheckOutDate < request.To ? RS.CheckOutDate : request.To).DayNumber
                        -
                        (RS.CheckInDate > request.From ? RS.CheckInDate : request.From).DayNumber
                        )) / totalNightsOnSelectedPeriod * 100, 2)
                        : 0
            }).ToListAsync();
            var countSpec = new RoomsOccupancySpec(request, false, false);
            var count = await roomRepo.GetQuery(countSpec).CountAsync();
            var obj = new GridsToReturnDto<OccupancyRoomsToReturnDto>()
            {
                Data = list,
                Total = count,
            };
            return obj;
        }
    }
}
