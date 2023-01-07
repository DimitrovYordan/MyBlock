using System.Collections.Generic;

using MyBlock.Models;
using MyBlock.Models.Relational_Classes;

namespace MyBlock.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        public List<Post> GetAll();
        public List<Post> GetAll(int id);
        public Post GetById(int id);
        PaginatedList<Post> FilterBy(PostQueryParams postQueryParams);
        public Post Create(Post post);
        public Post Update(int id, Post post);
        public void Delete(int id);
        public LikedByUser GetLikeId(int postId, int userId);
        public DislikedByUser GetDislikeId(int postId, int userId);
        public void AddLike(LikedByUser likedBy);
        public void AddDislike(DislikedByUser dislikedBy);
        public void RemoveLike(LikedByUser likedByUser);
        public void RemoveDislike(DislikedByUser dislikedByUser);
    }
}
