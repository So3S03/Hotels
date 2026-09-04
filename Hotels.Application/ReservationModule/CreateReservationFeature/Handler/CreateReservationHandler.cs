using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Application.Specifications.ReservationSpecs;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Reservations;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReservationModule;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.ReservationModule.CreateReservationFeature.Handler
{
    public class CreateReservationHandler(IUnitOfWork _unitOfWork, IReservationNotificationService _notifire, IMapper _mapper) : IRequestHandler<CreateReservationCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { RoomId: null or "" } => throw new BadRequest400Exception("Invalid RoomId"),
                { GuestName: null or ""} => throw new BadRequest400Exception("Invalid Guest Name"),
                _ => request
            };
            var currentDate = DateTime.UtcNow;
            var today = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);
            if (request.CheckInDate < today) throw new BadRequest400Exception("Reservation Range Can't Start From Old Date");
            if (request.CheckInDate >= request.CheckOutDate) throw new BadRequest400Exception("Check In Date Can't Be Greater Than Or Equal To Check Out Date");
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var roomSpec = new RoomByIdSpec(request.RoomId);
            var room = await roomRepo.GetAsync(roomSpec);
            if (room is null) throw new NotFound404Exception("Room Not Found");
            var reservationRepo = _unitOfWork.GenerateRepo<Reservation, string>();
            var reservationSpec = new RoomReservationsInDateRangeSpec(request.RoomId, request.CheckInDate, request.CheckOutDate);
            var reservationsExist = await reservationRepo.GetQuery(reservationSpec).AnyAsync();
            if (!room.IsAvailable || reservationsExist) throw new Conflict409Exception("This Room Is Not Available For Reservation");
            var countOfNight = request.CheckOutDate.DayNumber - request.CheckInDate.DayNumber;
            var calcTotal = room.PricePerNight * countOfNight;
            var mappedData = _mapper.Map<Reservation>(request);
            mappedData.TotalAmount = calcTotal;
            await reservationRepo.AddAsync(mappedData);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if (!result) throw new Exception("Something Went Wrong!");
            var notMapp = _mapper.Map<CreateReservationDto>(request);
            await _notifire.NotifyReservationCreation(notMapp, request.ConnectionId);
            var Obj = new ActionStatusDto()
            {
                Succeeded = true,
                Message = "Reservation Created Successfully"
            };
            return Obj;
        }
    }
}
