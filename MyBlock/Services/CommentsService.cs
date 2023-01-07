using System.Collections.Generic;

using MyBlock.Exceptions;
using MyBlock.Models;
using MyBlock.Services.Interfaces;
using MyBlock.Repositories.Interfaces;

namespace MyBlock.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository repository;

        public CommentsService(ICommentsRepository repository)
        {
            this.repository = repository;
        }

        public Comment Create(Comment comment)
        {
            return this.repository.Create(comment);
        }

        public void Delete(int id, User user)
        {
            Comment commentToDelete = this.GetById(id);
            if (commentToDelete.Author != user && !user.IsAdmin)
            {
                throw new UnauthorizedOperationException("");
            }
            else
            {
                this.repository.Delete(id);
            }

        }

        public Comment Update(int id, Comment updatedComment, User user)
        {
            Comment commentToUpdate = this.GetById(id);
            if (commentToUpdate.Author != user)
            {
                throw new UnauthorizedOperationException("");
            }
            else
            {
                return this.repository.Update(id, updatedComment);
            }

        }

        public List<Comment> GetAll()
        {
            return this.repository.GetAll();
        }

        public Comment GetById(int id)
        {
            return this.repository.GetById(id);
        }

    }
}
