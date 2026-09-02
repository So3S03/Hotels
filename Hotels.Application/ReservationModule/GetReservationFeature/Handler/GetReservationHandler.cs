using Hotels.Application.ReservationModule.GetReservationFeature.Query;
using Hotels.Application.Specifications.ReservationSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Reservations;
using Hotels.Shared.Dtos.ReservationModule;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;

namespace Hotels.Application.ReservationModule.GetReservationFeature.Handler
{
    public class GetReservationHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetReservationQuery, ReservationToReturnDto>
    {
        public async Task<ReservationToReturnDto> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ReservationId)) throw new BadRequest400Exception("Invalid Id");
            var reservationRepo = _unitOfWork.GenerateRepo<Reservation, string>();
            var resSpec = new ReservationById(request.ReservationId, true);
            var reservation = await reservationRepo.GetAsync(resSpec);
            if (reservation is null) throw new NotFound404Exception("Reservtion Not Exist");
            var mappedData = _mapper.Map<ReservationToReturnDto>(reservation);
            return mappedData;
        }
    }
}
