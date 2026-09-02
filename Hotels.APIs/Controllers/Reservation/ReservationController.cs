using Hotels.APIs.Controllers.Base;
using Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command;
using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Application.ReservationModule.GetReservationFeature.Query;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReservationModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Controllers.Reservation
{
    public class ReservationController(IMediator _mediator) : BaseApiController
    {
        [HttpPost("CreateReservation")]
        public async Task<ActionResult<ActionStatusDto>> CreateReservation(CreateReservationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("CancelReservation")]
        public async Task<ActionResult<ActionStatusDto>> CancelReservation([FromQuery]CancelReservationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("GetReservation")]
        public async Task<ActionResult<ReservationToReturnDto>> GetReservation([FromQuery]GetReservationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
