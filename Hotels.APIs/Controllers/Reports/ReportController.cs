using Hotels.APIs.Controllers.Base;
using Hotels.Application.ReportModule.OccupancyFeature.Query;
using Hotels.Application.ReportModule.RevenueFeature.Query;
using Hotels.Application.ReportModule.TopRoomsFeature.Query;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.ReportModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Controllers.Reports
{
    public class ReportController(IMediator _mediator) : BaseApiController
    {
        [HttpGet("GetTopNonCancelledRooms")]
        public async Task<ActionResult<GridsToReturnDto<TopNonCancelledRoomToReturnDto>>> GetTopNonCancelledRooms([FromQuery] GetTopNonCancelledRoomsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetRevenue")]
        public async Task<ActionResult<ICollection<RevenueToReturnDto>>> GetRevenue([FromQuery] GetRevenueQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetRoomOcuupancy")]
        public async Task<ActionResult<GridsToReturnDto<OccupancyRoomsToReturnDto>>> GetRoomOcuupancy([FromQuery] RoomsOccupancyQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
