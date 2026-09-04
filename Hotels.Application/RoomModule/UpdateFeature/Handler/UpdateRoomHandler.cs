using Hotels.Application.Abstraction.ServicesContracts;
using Hotels.Application.RoomModule.UpdateFeature.Command;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.RoomModule;
using Hotels.Shared.Errors;
using MapsterMapper;
using MediatR;

namespace Hotels.Application.RoomModule.UpdateFeature.Handler
{
    public class UpdateRoomHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IRoomNotificationService _notifire) : IRequestHandler<UpdateRoomCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {

            _ = request switch
            {
                { Id: null or "" } => throw new BadRequest400Exception("Room Id is required."),
                { RoomNumber: <= 0 } => throw new BadRequest400Exception("Invalid Room Number"),
                { RoomType: var type } when !Enum.IsDefined(typeof(RoomType), type) => throw new BadRequest400Exception("Invalid room type."),
                { PricePerNight: <= 0 } => throw new BadRequest400Exception("Price per night cannot be less than or equal to zero."),
                _ => request
            };
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var spec = new RoomByIdSpec(request.Id);
            var room = await roomRepo.GetAsync(spec);
            if(room is null) throw new NotFound404Exception($"Room with Id {request.Id} not found.");
            var mappedRoom = _mapper.Map(request, room);
            roomRepo.Update(mappedRoom);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if(!result) throw new Exception("Something Went Wrong");
            var mappedNot = _mapper.Map<ModifyRoomDto>(request);
            await _notifire.NotifyRoomModification(mappedNot, request.ConnectionId);
            var obj = new ActionStatusDto
            {
                Succeeded = true,
                Message = "Room updated successfully."
            };
            return obj;
        }
    }
}
