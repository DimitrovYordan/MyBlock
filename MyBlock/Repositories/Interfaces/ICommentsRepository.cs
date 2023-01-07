using System.Collections.Generic;

using MyBlock.Models;

namespace MyBlock.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        public List<Comment> GetAll();
        public Comment GetById(int id);
        public Comment Create(Comment comment);
        public Comment Update(int id, Comment comment);
        public void Delete(int id);
    }
}
