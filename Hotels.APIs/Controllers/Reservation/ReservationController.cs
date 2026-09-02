using Hotels.APIs.Controllers.Base;
using Hotels.Application.ReservationModule.ApproveCancelReservationFeature.Command;
using Hotels.Application.ReservationModule.CreateReservationFeature.Command;
using Hotels.Shared.Dtos._Common;
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
    }
}
