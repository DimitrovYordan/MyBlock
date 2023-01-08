using MyBlock.Helpers;
using MyBlock.Models;
using MyBlock.Exceptions;
using MyBlock.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace MyBlock.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthManager authManager;
        private readonly IUsersService usersService;
        private readonly IMapper modelMapper;

        public HomeController(AuthManager authManager, IUsersService usersService, IMapper modelMapper)
        {
            this.authManager = authManager;
            this.usersService = usersService;
            this.modelMapper = modelMapper;
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.authManager.Login(model);

                return RedirectToAction("Index", "Post");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.ModelState.AddModelError("Password", ex.Message);

                return this.View(model);
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.authManager.Logout();

            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this.usersService.UsernameExists(model.Username))
            {
                this.ModelState.AddModelError("Username", "User with same username already exists.");

                return this.View(model);
            }

            if (!this.usersService.PasswordIsComplex(model.Password))
            {
                this.ModelState.AddModelError("Password", "Password must be beteween 4-40 characters!");

                return this.View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                return this.View(model);
            }

            try
            {
                if (!this.usersService.EmailIsValid(model.Email))
                {
                    this.ModelState.AddModelError("Email", "Please enter a valid email");

                    return this.View(model);
                }

            }
            catch (UnauthorizedOperationException ex)
            {
                this.ModelState.AddModelError("Email", ex.Message);

                return this.View(model);
            }

            User user = this.modelMapper.MapUser(model);
            this.usersService.Create(user);

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Documentation()
        {
            return View();
        }
    }
}