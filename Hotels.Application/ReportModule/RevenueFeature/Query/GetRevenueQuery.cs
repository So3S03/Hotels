using Hotels.Application._Common;
using Hotels.Shared.Dtos.ReportModule;
using MediatR;

namespace Hotels.Application.ReportModule.RevenueFeature.Query
{
    public class GetRevenueQuery : IRequest<ICollection<RevenueToReturnDto>>
    {
        public required DateOnly From { get; set; }
        public required DateOnly To { get; set; }
    }
}
