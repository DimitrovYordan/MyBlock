using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlock.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [ForeignKey("Author")]
        public int? AuthorID { get; set; }
        public User Author { get; set; }
        [ForeignKey("ParentPost")]
        public int ParentPostID { get; set; }
        public Post ParentPost { get; set; }
        [ForeignKey("ParentComment")]
        public int? ParentCommentID { get; set; }
        public Comment ParentComment { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
