using System.Collections.Generic;

using MyBlock.Models.Relational_Classes;

namespace MyBlock.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<LikedByUser> LikedPosts { get; set; }
        public List<DislikedByUser> DislikedPosts { get; set; }
    }
}
