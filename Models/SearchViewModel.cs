namespace CahootSOOA.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public string SearchQuery { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
