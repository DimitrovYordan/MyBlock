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

        public AuthManager(IUsersService users, IHttpContextAccessor contextAccessor)
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
            catch (EntityNotFoundExcception)
            {
                throw new UnauthorizedOperationException("User not found!");
            }

        }

    }
}
