namespace Hotels.Shared.Errors
{
    public class NotFound404Exception : Exception
    {
        public NotFound404Exception() : base("Entity Not Found")
        {
            
        }
        public NotFound404Exception(string? message): base(message)
        {
            
        }
    }
}
