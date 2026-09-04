using Hotels.APIs.Controllers.Base;
using Hotels.Application._Common;
using Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command;
using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Application.ReservationModule.GetAllReservationsFeature.Query;
using Hotels.Application.ReservationModule.GetReservationFeature.Query;
using Hotels.Application.ReservationModule.GetReservationLogFeature.Handler;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.LogsModule;
using Hotels.Shared.Dtos.ReservationModule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Controllers.Reservation
{
    [Authorize]
    public class ReservationController(IMediator _mediator) : BaseApiController
    {
        [HttpPost("CreateReservation")]
        public async Task<ActionResult<ActionStatusDto>> CreateReservation(CreateReservationCommand command, [FromHeader(Name = "SignalR-Connection-Id")] string? connectionId)
        {
            command.ConnectionId = connectionId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("CancelReservation")]
        public async Task<ActionResult<ActionStatusDto>> CancelReservation([FromQuery]CancelReservationCommand command, [FromHeader(Name = "SignalR-Connection-Id")] string? connectionId)
        {
            command.ConnectionId = connectionId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetReservation")]
        public async Task<ActionResult<ReservationToReturnDto>> GetReservation([FromQuery]GetReservationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetAllReservation")]
        public async Task<ActionResult<CommonGridQuery<ReservationToReturnDto>>> GetAllReservation([FromQuery]GetAllReservationsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetReservationsLog")]
        public async Task<ActionResult<CommonGridQuery<LogToReturnDto>>> GetReservationsLog([FromQuery]GetReservationLogHandler query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
