using System;

namespace MyBlock.Models.DataTransferObjects
{
    public class CommentDto
    {
        public int? AuthorID { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int ParentPostID { get; set; }
        public int? ParentCommentID { get; set; }
        public DateTime TimePosted { get; set; }
    }
}