using Hotels.Application.ReservationModule.GetReservationLogFeature.Query;
using Hotels.Application.Specifications.Base;
using Hotels.Domain.Entities.BaseEntities;

namespace Hotels.Application.Specifications.ReservationSpecs
{
    internal class ReservationsLogSpec : BaseSpecification<AuditLog>
    {
        public ReservationsLogSpec(GetReservationLogQuery query, bool isPagination = true): base(
                L => L.EntityId == query.ReservationId && L.EntityName == typeof(AuditLog).Name
            )
        {
            if ((query.PageNum > 0 || query.PageSize > 0) && isPagination) Pagination(query.PageNum > 0 ? query.PageNum : 1, query.PageSize > 0 ? query.PageSize : 5);
        }
    }
}
