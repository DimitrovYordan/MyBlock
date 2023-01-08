using System.Collections.Generic;

using MyBlock.Models;

namespace MyBlock.Services.Interfaces
{
    public interface IUsersService
    {
        public User Create(User newUser);
        public User Update(int id, User updateUser, string username);
        public User Update(int id, ChangePasswordViewModel changePasswird);
        public List<User> GetAll();
        public User GetById(int id);
        public User GetByUsername(string username);
        public bool UsernameExists(string username);
        public bool PasswordIsComplex(string password);
        public bool EmailIsValid(string email);
    }
}