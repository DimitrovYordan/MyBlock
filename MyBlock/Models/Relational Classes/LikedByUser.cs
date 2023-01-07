using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlock.Models.Relational_Classes
{
    public class LikedByUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
