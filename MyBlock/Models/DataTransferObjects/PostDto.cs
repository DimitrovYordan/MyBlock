using System;

namespace MyBlock.Models.DataTransferObjects
{
    public class PostDto
    {
        public int AuthorID { get; set; }
        public string AuthorUsername { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentsCount { get; set; }
        public DateTime TimePosted { get; set; }
    }
}