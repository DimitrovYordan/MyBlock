using System;
using System.Collections.Generic;

using MyBlock.Models;
using MyBlock.Models.Relational_Classes;
using MyBlock.Services.Interfaces;
using MyBlock.Repositories.Interfaces;

namespace MyBlock.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository repository;

        public PostsService(IPostsRepository repository)
        {
            this.repository = repository;
        }

        public List<Post> GetAll()
        {
            return this.repository.GetAll();
        }

        public List<Post> GetAll(int id)
        {
            return this.repository.GetAll(id);
        }

        public Post GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public Post Create(Post post)
        {
            post.TimePosted = System.DateTime.Now;
            return this.repository.Create(post);
        }

        public Post Update(int id, Post updatedPost, User user)
        {
            Post postToUpdate = this.GetById(id);
            if (postToUpdate.Author != user)
            {
                throw new UnauthorizedAccessException("");
            }
            else
            {
                return this.repository.Update(id, updatedPost);
            }

        }

        public void Delete(int id, User user)
        {
            Post postToDelete = this.GetById(id);
            if (postToDelete.Author != user && !user.IsAdmin)
            {
                throw new UnauthorizedAccessException("");
            }
            else
            {
                this.repository.Delete(id);
            }

        }

        public PaginatedList<Post> FilterBy(PostQueryParams postQueryParams)
        {
            return this.repository.FilterBy(postQueryParams);
        }

        public void AddLike(LikedByUser likedBy)
        {
            this.repository.AddLike(likedBy);
        }

        public void AddDislike(DislikedByUser dislikeBy)
        {
            this.repository.AddDislike(dislikeBy);
        }

        public LikedByUser GetLikeId(int postId, int userId)
        {
            return this.repository.GetLikeId(postId, userId);
        }

        public DislikedByUser GetDislikeId(int postId, int userId)
        {
            return this.repository.GetDislikeId(postId, userId);
        }

        public void RemoveLike(LikedByUser likedByUser)
        {
            this.repository.RemoveLike(likedByUser);
        }

        public void RemoveDislike(DislikedByUser dislikedByUser)
        {
            this.repository.RemoveDislike(dislikedByUser);
        }

    }
}
