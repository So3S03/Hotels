using Hotels.Application.RoomModule.CreateFeature.Command;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.RoomModule.CreateFeature.Handler
{
    public class CreateRoomHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreateRoomCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { RoomNumber: <= 0 } => throw new BadRequest400Exception("Room number must be greater than zero."),
                { RoomType: var type } when !Enum.IsDefined(typeof(RoomType), type) => throw new BadRequest400Exception("Invalid room type."),
                { PricePerNight: <= 0 } => throw new BadRequest400Exception("Price per night must be greater than zero."),
                _ => request
            };
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var Spec = new RoomByRoomNumber(request.RoomNumber);
            var roomExist = await roomRepo.GetQuery(Spec).AnyAsync();
            if(roomExist) throw new Conflict409Exception($"Room with number {request.RoomNumber} already exists.");
            var mappedRoom = _mapper.Map<Room>(request);
            mappedRoom.IsAvailable = true;
            await roomRepo.AddAsync(mappedRoom);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if(!result) throw new Exception("Something Went Wrong");
            var Obj = new ActionStatusDto()
            {
                Succeeded = true,
                Message = "Room Created Successfully"
            };
            return Obj;
        }
    }
}
