using Hotels.Application.ReportModule.RevenueFeature.Query;
using Hotels.Application.Specifications.ReportSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReportModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReportModule.RevenueFeature.Handler
{
    public class GetRevenueHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetRevenueQuery, ICollection<RevenueToReturnDto>>
    {
        public async Task<ICollection<RevenueToReturnDto>> Handle(GetRevenueQuery request, CancellationToken cancellationToken)
        {
            var roomRepo = _unitOfWork.GenerateRepo<Reservation, string>();
            var roomSpec = new RoomsRevenueInDateRangeSpec(request);
            var list = await roomRepo.GetQuery(roomSpec).GroupBy(R => R.Room.RoomType).Select(x => new RevenueToReturnDto()
            {
                StartDate = x.Min(RS => RS.CheckInDate),
                EndDate = x.Max(RS => RS.CheckOutDate),
                RoomType = x.Key.ToString(),
                TotalNights = x.Sum(RS => RS.CheckOutDate.DayNumber - RS.CheckInDate.DayNumber),
                TotalReservations = x.Count(),
                TotalRevenue = x.Sum(rs => rs.TotalAmount)
            }).ToListAsync();
            return list;
        }
    }
}
