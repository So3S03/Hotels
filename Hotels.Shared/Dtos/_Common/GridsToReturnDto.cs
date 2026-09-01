namespace Hotels.Shared.Dtos._Common
{
    public class GridsToReturnDto<T>
    {
        public int Total { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
