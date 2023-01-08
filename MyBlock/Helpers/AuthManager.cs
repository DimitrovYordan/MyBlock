using MyBlock.Services.Interfaces;
using MyBlock.Exceptions;
using MyBlock.Models;

using Microsoft.AspNetCore.Http;

namespace MyBlock.Helpers
{
    public class AuthManager
    {
        private readonly string currentUser = "CURRENT_USER";
        private readonly IUsersService usersService;
        private readonly IHttpContextAccessor sessionAccessor;

        public AuthManager(IUsersService usersService, IHttpContextAccessor sessionAccessor)
        {
            this.usersService = usersService;
            this.sessionAccessor = sessionAccessor;
        }

        public virtual User TryGetUser(string username)
        {
            try
            {
                return this.usersService.GetByUsername(username);
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException("User not found!");
            }

        }

        public virtual User TryGetUser (LoginViewModel model)
        {
            try
            {
                User user = this.usersService.GetByUsername(model.Username);
                if (user.Password != model.Password)
                {
                    throw new EntityNotFoundException();
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new UnauthorizedOperationException("Invalid username or password.");
            }

        }

        public void Login (LoginViewModel model)
        {
            this.CurrentUser = this.TryGetUser(model);
        }

        public void Logout()
        {
            this.CurrentUser = null;
        }

        public User CurrentUser
        {
            get
            {
                try
                {
                    string username = this.sessionAccessor.HttpContext.Session.GetString(currentUser);
                    return this.usersService.GetByUsername(username);
                }
                catch (EntityNotFoundException)
                {
                    return null;
                }

            }
            set
            {
                if (value != null)
                {
                    this.sessionAccessor.HttpContext.Session.SetString(currentUser, value.Username);
                }
                else
                {
                    this.sessionAccessor.HttpContext.Session.Remove(currentUser);
                }

            }

        }

    }
}