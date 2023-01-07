using System.Collections.Generic;
using System.Linq;

using MyBlock.Data;
using MyBlock.Models;
using MyBlock.Repositories.Interfaces;
using MyBlock.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace MyBlock.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly MyBlockContext context;

        public CommentsRepository(MyBlockContext context)
        {
            this.context = context;
        }

        public List<Comment> GetAll()
        {
            return this.GetComments().ToList();
        }

        public Comment GetById(int id)
        {
            Comment comment = this.GetComments().Where(c => c.Id == id).FirstOrDefault();
            return comment ?? throw new EntityNotFoundException();
        }

        public Comment Create(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();

            return comment;
        }

        public Comment Update(int id, Comment comment)
        {
            Comment commentToUpdate = this.GetById(id);
            commentToUpdate.Content = comment.Content;
            this.context.Update(commentToUpdate);
            this.context.SaveChanges();

            return commentToUpdate;
        }

        public void Delete(int id)
        {
            Comment commentToDelete = this.GetById(id);
            this.context.Comments.Remove(commentToDelete);
            this.context.SaveChanges();
        }

        public IQueryable<Comment> GetComments()
        {
            return this.context.Comments.Include(comment => comment.Author);
        }

    }
}
