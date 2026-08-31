namespace Hotels.Shared.Errors
{
    public class BadRequest400Exception : Exception
    {
        public BadRequest400Exception() : base("Bad Request Check On The Data")
        {
            
        }
        public BadRequest400Exception(string? message): base(message)
        {
            
        }
    }
}
