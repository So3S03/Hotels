namespace Hotels.Shared.Errors
{
    public class Unauthorized401Exception : Exception
    {
        public Unauthorized401Exception(): base("You'r Not Authunticated")
        {
            
        }
        public Unauthorized401Exception(string message): base(message)
        {
            
        }
    }
}
