using Microsoft.AspNetCore.Mvc;

namespace MyBlock.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
