using System.Collections.Generic;

using MyBlock.Models;

namespace MyBlock.Services.Interfaces
{
    public interface ICommentsService
    {
        public Comment Create(Comment newUser);
        public void Delete(int id, User user);
        public Comment Update(int id, Comment comment, User user);
        public List<Comment> GetAll();
        public Comment GetById(int id);
    }
}
