using Microsoft.AspNetCore.Mvc;

namespace MyBlock.Controllers
{
    public class UserController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
