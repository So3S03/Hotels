using Hotels.Application.ReservationModule.GetAllReservationsFeature.Query;
using Hotels.Application.Specifications.ReservationSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Reservations;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReservationModule;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReservationModule.GetAllReservationsFeature.Handler
{
    public class GetAllReservationHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllReservationsQuery, GridsToReturnDto<ReservationToReturnDto>>
    {
        public async Task<GridsToReturnDto<ReservationToReturnDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservationRepo = _unitOfWork.GenerateRepo<Reservation, string>();
            var reservationSpec = new ReservationListSpec(request);
            var reservationCountSpec = new ReservationListSpec(request, false);
            var reservationList = await reservationRepo.GetAllAsync(reservationSpec);
            var reservationCount = await reservationRepo.GetQuery(reservationCountSpec).CountAsync();
            var mappedList = _mapper.Map<ICollection<ReservationToReturnDto>>(reservationList);
            var obj = new GridsToReturnDto<ReservationToReturnDto>()
            {
                Data = mappedList,
                Total = reservationCount
            };
            return obj;
        }
    }
}
