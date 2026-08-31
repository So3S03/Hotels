namespace Hotels.Shared.Errors
{
    public class MethodNotAllowed405Exception : Exception
    {
        public MethodNotAllowed405Exception(): base("The Http Method Not Aligned With The Api Method")
        {
            
        }
        public MethodNotAllowed405Exception(string message) : base(message)
        {
            
        }
    }
}
