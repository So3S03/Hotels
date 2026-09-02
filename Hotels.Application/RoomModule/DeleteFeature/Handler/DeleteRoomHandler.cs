using Hotels.Application.RoomModule.DeleteFeature.Command;
using Hotels.Application.Specifications.RoomSpecs;
using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.Room;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Application.RoomModule.DeleteFeature.Handler
{
    public class DeleteRoomHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteRoomCommand, ActionStatusDto>
    {
        public async Task<ActionStatusDto> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            _ = request switch
            {
                { RoomId: null or "" } => throw new BadRequest400Exception("Invalid RoomId"),
                _ => request
            };
            var roomRepo = _unitOfWork.GenerateRepo<Room, string>();
            var spec = new RoomByIdSpec(request.RoomId);
            var room = await roomRepo.GetAsync(spec);
            if(room is null) throw new NotFound404Exception($"Room with id {request.RoomId} not found");
            if(!room.IsAvailable) throw new Conflict409Exception($"Can't delete room with confirmed reservations");
            roomRepo.Delete(room);
            var result = await _unitOfWork.CompleteAsync() > 0;
            if(!result) throw new Exception("Something Went Wrong");
            var Obj = new ActionStatusDto()
            {
                Succeeded = true,
                Message = "Room Deleted Successfully"
            };
            return Obj;
        }
    }
}
