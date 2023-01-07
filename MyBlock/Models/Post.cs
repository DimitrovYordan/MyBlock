using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MyBlock.Models.Relational_Classes;

namespace MyBlock.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(16, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(8192, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(32, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string Content { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public User Author { get; set; }
        public int Rating { get; set; }
        public DateTime TimePosted { get; set; }
        public List<Comment> Comments { get; set; }
        public List<LikedByUser> LikedByUsers { get; set; }
        public List<DislikedByUser> DislikedByUsers { get; set; }
    }
}
