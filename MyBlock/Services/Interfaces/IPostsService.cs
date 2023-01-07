using System.Collections.Generic;

using MyBlock.Models;
using MyBlock.Models.Relational_Classes;

namespace MyBlock.Services.Interfaces
{
    public interface IPostsService
    {
        List<Post> GetAll();
        List<Post> GetAll(int id);
        PaginatedList<Post> FilterBy(PostQueryParams postQueryParams);
        Post GetById(int id);
        Post Create(Post post);
        Post Update(int id, Post post, User user);
        void Delete(int id, User user);
        public LikedByUser GetLikeId(int postId, int userId);
        public DislikedByUser GetDislikeId(int postId, int userId);
        public void AddLike(LikedByUser likedBy);
        public void AddDislike(DislikedByUser dislikeBy);
        public void RemoveLike(LikedByUser likedByUser);
        public void RemoveDislike(DislikedByUser dislikedByUser);
    }
}
