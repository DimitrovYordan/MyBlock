using MyBlock.Helpers;
using MyBlock.Services.Interfaces;
using MyBlock.Exceptions;
using MyBlock.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyBlock.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService usersService;
        private readonly AuthManager authManager;

        public UserController(IUsersService usersService, AuthManager authManager)
        {
            this.usersService = usersService;
            this.authManager = authManager;
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                var user = this.usersService.GetById(id);
                return this.View(user);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpGet]
        public IActionResult Settings()
        {
            var model = new ChangePasswordViewModel();

            return this.View(model);
        }

        [HttpPost, ActionName("ChangePassword")]
        public IActionResult Settings(ChangePasswordViewModel changePassword)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError("Password", "New password is too short!");

                return this.RedirectToAction("Settings", "User");
            }

            if (this.authManager.CurrentUser.Password != changePassword.OldPassword)
            {
                this.ModelState.AddModelError("Password", "Incorrect paswword!");

                return this.View(changePassword);
            }

            if (!this.usersService.PasswordIsComplex(changePassword.NewPassword))
            {
                this.ModelState.AddModelError("Password", "Password must be between 4-40 characters!");

                return this.View(changePassword);
            }

            if (changePassword.NewPassword != changePassword.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");

                return this.View(changePassword);
            }

            this.usersService.Update(this.authManager.CurrentUser.Id, changePassword);

            return this.RedirectToAction("Settings", "User");
        }

    }
}