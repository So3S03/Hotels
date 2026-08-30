namespace Hotels.Domain.Entities.BaseEntities
{
    public class AuditLog : BaseEntity<string>
    {
        public string TableName { get; set; }
        public string TableId { get; set; }
        public ActionType ActionType { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
