namespace Hotels.Shared.Errors
{
    public class Conflict409Exception : Exception
    {
        public Conflict409Exception(): base("Conflict Happen While Create/Update The Entity")
        {
            
        }

        public Conflict409Exception(string messaage): base(messaage)
        {
            
        }
    }
}
