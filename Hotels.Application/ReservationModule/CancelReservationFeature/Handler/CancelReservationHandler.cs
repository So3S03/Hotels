using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command;
using Hotels.Application.Specifications.ReservationSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Reservations;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReservationModule;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;

namespace Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Handler
{
    public class CancelReservationHandler(IUnitOfWork _unitOfWork, IReservationNotificationService _notifire, IMapper _mapper) : IRequestHandler<CancelReservationCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ReservationId)) throw new BadRequest400Exception("Invalid Reservation Id");
            var reservationRepo = _unitOfWork.GenerateRepo<Reservation, string>();
            var reservationSpec = new ReservationById(request.ReservationId);
            var reservation = await reservationRepo.GetAsync(reservationSpec);
            if (reservation is null) throw new NotFound404Exception("Reservation Not Exist!");
            if (reservation.Status == ReservationStatus.Cancelled) throw new Conflict409Exception("Reservation Already Cancelled");
            reservation.Status = ReservationStatus.Cancelled;
            var result = await _unitOfWork.CompleteAsync() > 0;
            if (!result) throw new Exception("Something Went Wrong");
            var mappedNotifireDto = _mapper.Map<ReservationToReturnDto>(reservation);
            await _notifire.NotifyReservationCancellation(mappedNotifireDto, request.ConnectionId);
            var Obj = new ActionStatusDto()
            {
                Succeeded = true,
                Message = "Reservation Cancelled Successfully"
            };
            return Obj;
        }
    }
}
