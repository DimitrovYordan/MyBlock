using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using MyBlock.Exceptions;
using MyBlock.Models;
using MyBlock.Services.Interfaces;
using MyBlock.Repositories.Interfaces;

namespace MyBlock.Services
{
    public class UsersService : IUsersService
    {
        private readonly Regex PasswordRegexValidator = new Regex(@".{4,40}");
        private readonly Regex EmailRegexValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private readonly IUsersRepository repository;

        public UsersService(IUsersRepository repository)
        {
            this.repository = repository;
        }

        public List<User> GetAll()
        {
            return this.repository.GetAll();
        }

        public User GetById(int id)
        {
            return this.repository.GetById(id);
        }

        public User GetByUsername(string username)
        {
            return this.repository.GetByUsername(username);
        }

        public User Create(User newUser)
        {
            //Checks is username already exists
            if (this.repository.GetAll().Exists(x => x.Username == newUser.Username))
            {
                throw new DuplicateEntityException();
            }

            //Create new user
            return this.repository.Create(newUser);
        }

        public User Update(int id, User updateUser, string username)
        {
            User userToUpdate = this.GetById(id);
            if (userToUpdate.Username != username && this.GetByUsername(username).IsAdmin == false)
            {
                throw new UnauthorizedOperationException("Delete operation denied.");
            }

            return this.repository.Update(id, userToUpdate);
        }

        public User Update(int id, ChangePasswordViewModel changePasswird)
        {
            return this.repository.Update(id, changePasswird);
        }

        public void Delete(int id, string username)
        {
            User userToDelete = this.GetById(id);
            if (userToDelete.Username != username && this.GetByUsername(username).IsAdmin == false)
            {
                throw new UnauthorizedOperationException("Delete operation denied.");
            }
            else
            {
                this.repository.Delete(id);
            }

        }

        public bool UsernameExists(string username)
        {
            bool usernameExists = true;

            try
            {
                _ = this.repository.GetByUsername(username);
            }
            catch (EntityNotFoundException)
            {
                usernameExists = false;
            }

            return usernameExists;
        }

        public bool EmailIsValid(string email)
        {
            List<User> emails = this.GetAll().Where(x => x.Email == email).ToList();
            if (emails.Any())
            {
                throw new UnauthorizedOperationException("Email already in use.");
            }

            if (EmailRegexValidator.IsMatch(email))
            {
                return true;
            }

            return false;
        }

        public bool PasswordIsComplex(string password)
        {
            if (PasswordRegexValidator.IsMatch(password))
            {
                return true;
            }

            return false;
        }

    }
}
