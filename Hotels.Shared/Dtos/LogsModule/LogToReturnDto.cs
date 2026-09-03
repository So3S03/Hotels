namespace Hotels.Shared.Dtos.LogsModule
{
    public class LogToReturnDto
    {
        public string Id { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string ActionTypeName { get; set; }
        public string ActionTypeId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
