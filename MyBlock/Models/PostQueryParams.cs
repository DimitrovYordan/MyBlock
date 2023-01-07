namespace MyBlock.Models
{
    public class PostQueryParams
    {
        public string Title { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
