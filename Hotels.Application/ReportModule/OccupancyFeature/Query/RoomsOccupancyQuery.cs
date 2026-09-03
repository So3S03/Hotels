using Hotels.Application._Common;
using Hotels.Shared.Dtos.ReportModule;

namespace Hotels.Application.ReportModule.OccupancyFeature.Query
{
    public class RoomsOccupancyQuery : CommonGridQuery<OccupancyRoomsToReturnDto>
    {
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
