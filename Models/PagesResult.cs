using System.Collections;

namespace api_wov.Models
{
    public class PagesResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
        public Paginations Pages { get; set; }
        public PagesResult() 
        {
            TotalCount = 0;
            Items = new List<T>();
            Pages = new Paginations();
        }
    }
}
