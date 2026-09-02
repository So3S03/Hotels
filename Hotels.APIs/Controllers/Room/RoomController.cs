using Hotels.APIs.Controllers.Base;
using Hotels.Application.RoomModule.CreateFeature.Command;
using Hotels.Application.RoomModule.DeleteFeature.Command;
using Hotels.Application.RoomModule.GetAllRoomsFeature.Query;
using Hotels.Application.RoomModule.GetRoomFeature.Query;
using Hotels.Application.RoomModule.UpdateFeature.Command;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.RoomModule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Controllers.Room
{
    [Authorize]
    public class RoomController(IMediator _mediator) : BaseApiController
    {
        [HttpPost("AddRoom")]
        public async Task<ActionResult<ActionStatusDto>> AddRoom(CreateRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateRoom")]
        public async Task<ActionResult<ActionStatusDto>> UpdateRoom(UpdateRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteRoom")]
        public async Task<ActionResult<ActionStatusDto>> DeleteRoom([FromQuery]DeleteRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetRoomById")]
        public async Task<ActionResult<RoomToReturnDto>> GetRoomById([FromQuery] GetRoomQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<GridsToReturnDto<RoomToReturnDto>>> GetAllRooms([FromQuery]GetAllRoomsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
