using Hotels.APIs.Controllers.Base;
using Hotels.Application.RoomModule.CreateFeature.Command;
using Hotels.Application.RoomModule.DeleteFeature.Command;
using Hotels.Application.RoomModule.UpdateFeature.Command;
using Hotels.Shared.Dtos._Common;
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
    }
}
