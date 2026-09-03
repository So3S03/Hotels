using Hotels.Application._Common;
using Hotels.Shared.Dtos.LogsModule;
using MediatR;

namespace Hotels.Application.ReservationModule.GetReservationLogFeature.Query
{
    public class GetReservationLogQuery : CommonGridQuery<LogToReturnDto>
    {
        public string ReservationId { get; set; }
    }
}
