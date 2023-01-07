using System.Collections.Generic;

using MyBlock.Models;

namespace MyBlock.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public User GetById(int id);
        public User GetByUsername(string username);
        public List<User> GetAll();
        public User Create(User user);
        public User Update(int id, User user);
        public User Update(int id, ChangePasswordViewModel changePassword);
        public void Delete(int id);
    }
}
