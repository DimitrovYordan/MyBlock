using System.Collections.Generic;
using System.Linq;

using MyBlock.Data;
using MyBlock.Exceptions;
using MyBlock.Models;
using MyBlock.Repositories.Interfaces;

namespace MyBlock.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyBlockContext context;

        public UsersRepository(MyBlockContext context)
        {
            this.context = context;
        }

        public List<User> GetAll()
        {
            return this.GetUsers().ToList();
        }

        public User GetById(int id)
        {
            User user = this.GetUsers().Where(u => u.Id == id).FirstOrDefault();
            return user ?? throw new EntityNotFoundException();
        }

        public User GetByUsername(string username)
        {
            User user = this.GetUsers().Where(u => u.Username == username).FirstOrDefault();
            return user ?? throw new EntityNotFoundException();
        }

        public User Create(User user)
        {
            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User Update(int id, User user)
        {
            User userToUpdate = this.GetById(id);
            if (GetAll().Exists(user => user.Username == user.Username))
            {
                throw new DuplicateEntityException();
            }

            userToUpdate.Username = user.Username;
            userToUpdate.Password = user.Password;
            this.context.Update(userToUpdate);
            this.context.SaveChanges();

            return userToUpdate;
        }

        public User Update(int id, ChangePasswordViewModel changePassword)
        {
            User passwordToUpdate = this.GetById(id);
            passwordToUpdate.Password = changePassword.NewPassword;
            this.context.Update(passwordToUpdate);
            this.context.SaveChanges();

            return passwordToUpdate;
        }

        public IQueryable<User> GetUsers()
        {
            return this.context.Users;
        }
    }
}
