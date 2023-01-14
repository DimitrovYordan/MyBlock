using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using MyBlock.Models;
using MyBlock.Models.Relational_Classes;

namespace MyBlock.Data
{
    public class MyBlockContext : DbContext
    {
        public MyBlockContext(DbContextOptions<MyBlockContext> options)
            : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LikedByUser> LikedByUser { get; set; }
        public DbSet<DislikedByUser> DislikedByUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<User> users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Email = "pesho@gmail.com",
                    Username = "PeshoP",
                    Password = "pass123",
                    IsAdmin = true
                },
                new User
                {
                    Id = 2,
                    FirstName = "Gosho",
                    LastName = "Goshov",
                    Email = "gosho@gmail.com",
                    Username = "GoshoG",
                    Password = "pass123",
                    IsAdmin = false
                }
            };
            List<Comment> comments = new List<Comment>()
            {
                new Comment
                {
                    AuthorID = 1,
                    Id = 1,
                    Content = "Tova e 1 komentar",
                    TimePosted = DateTime.Now,
                    ParentPostID = 1,
                    ParentCommentID = null
                },
                new Comment
                {
                    AuthorID = 2,
                    Id = 2,
                    Content = "Tova e 2 komentar",
                    TimePosted = DateTime.Now,
                    ParentPostID = 2,
                    ParentCommentID = null
                }
            };
            List<Post> posts = new List<Post>()
            {
                new Post
                {
                    Id = 1,
                    Title = "Purvi post",
                    Content = "Siguren li si?",
                    AuthorID = 1,
                    TimePosted = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Title = "Vtori post",
                    Content = "Nqma da stane?",
                    AuthorID = 2,
                    TimePosted = DateTime.Now
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Comment>().HasData(comments);
            modelBuilder.Entity<Post>().HasData(posts);
        }

    }
}