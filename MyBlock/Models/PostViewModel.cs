namespace MyBlock.Models
{
    public class PostViewModel : Post
    {
        public string newComment { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}