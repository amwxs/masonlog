namespace SharpMason.Extensions
{
    public abstract class Pager
    {
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public Pager(int total, int pageSize, int pageIndex)
        {
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
