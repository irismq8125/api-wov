namespace api_wov.Models
{
    public class Paginations
    {
        public int TotalPage { get; set; } = 1;
        public int PageSizes { get; set; } = 10;
        public int PageCurrent { get; set; } = 1;
        public bool PagePrevious { get; set; } = false;
        public bool PageNext { get; set; } = true;
    }
}
