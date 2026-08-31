namespace Hotels.Domain.Entities.BaseEntities
{
    public class AuditLog : BaseEntity<string>
    {
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public ActionType ActionType { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
