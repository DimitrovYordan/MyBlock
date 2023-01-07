using System;
using System.Linq;
using System.Collections.Generic;

using MyBlock.Data;
using MyBlock.Exceptions;
using MyBlock.Models;
using MyBlock.Models.Relational_Classes;
using MyBlock.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace MyBlock.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly MyBlockContext context;

        public PostsRepository(MyBlockContext context)
        {
            this.context = context;
        }

        public List<Post> GetAll()
        {
            return this.GetPosts().ToList();
        }

        public List<Post> GetAll(int id)
        {
            return this.GetPosts(id).ToList();
        }

        public Post GetById(int id)
        {
            Post post = this.GetPosts().Where(p => p.Id == id).FirstOrDefault();
            return post ?? throw new EntityNotFoundException();
        }

        public Post Create(Post post)
        {
            this.context.Posts.Add(post);
            this.context.SaveChanges();

            return post;
        }

        public Post Update(int id, Post post)
        {
            Post postToUpdate = this.GetById(id);
            postToUpdate.Content = post.Content;
            this.context.Update(postToUpdate);
            this.context.SaveChanges();

            return postToUpdate;
        }

        public void Delete(int id)
        {
            Post postToDelete = this.GetById(id);
            this.context.Posts.Remove(postToDelete);
            this.context.SaveChanges();
        }

        public PaginatedList<Post> FilterBy(PostQueryParams postQueryParams)
        {
            IQueryable<Post> posts = this.GetPosts();
            posts = FilterByTitle(posts, postQueryParams.Title);
            posts = posts.OrderByDescending(post => post.Rating).ThenByDescending(post => post.Comments.Count);

            int totalPages = (int)Math.Ceiling((decimal)(posts.Count()) / (decimal)postQueryParams.PageSize);
            posts = Paginate(posts, postQueryParams.PageNumber, postQueryParams.PageSize);

            return new PaginatedList<Post>(posts.ToList(), totalPages, postQueryParams.PageNumber);
        }

        public void AddLike(LikedByUser likedBy)
        {
            this.context.LikedByUser.Add(likedBy);
            this.context.SaveChanges();
        }

        public void AddDislike(DislikedByUser dislikedBy)
        {
            this.context.DislikedByUsers.Add(dislikedBy);
            this.context.SaveChanges();
        }

        public void RemoveLike(LikedByUser likedByUser)
        {
            this.context.LikedByUser.Remove(likedByUser);
            this.context.SaveChanges();
        }

        public void RemoveDislike(DislikedByUser dislikedByUser)
        {
            this.context.DislikedByUsers.Remove(dislikedByUser);
            this.context.SaveChanges();
        }

        public LikedByUser GetLikeId(int postId, int userId)
        {
            return this.GetLikedByUsers().Where(x => x.PostId == postId).Where(x => x.UserId == userId).FirstOrDefault();
        }

        public DislikedByUser GetDislikeId(int postId, int userId)
        {
            return this.GetDislikedByUsers().Where(x => x.PostId == postId).Where(x => x.UserId == userId).FirstOrDefault();
        }

        public IQueryable<Post> GetPosts(int id)
        {
            return this.context.Posts.Where(x => x.AuthorID == id);
        }
        public IQueryable<Post> GetPosts()
        {
            return this.context.Posts
                .Include(post => post.Author)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Author)
                .Include(post => post.LikedByUsers)
                .Include(post => post.DislikedByUsers);
        }

        public IQueryable<Post> FilterByTitle(IQueryable<Post> posts, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return posts.Where(post => post.Title.Contains(title));
            }
            else
            {
                return posts;
            }

        }

        public IQueryable<Post> Paginate(IQueryable<Post> posts, int pageNumber, int pageSize)
        {
            return posts.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<LikedByUser> GetLikedByUsers()
        {
            return this.context.LikedByUser;
        }

        public IQueryable<DislikedByUser> GetDislikedByUsers()
        {
            return this.context.DislikedByUsers;
        }

    }
}
