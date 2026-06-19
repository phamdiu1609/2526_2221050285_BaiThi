using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class HelloController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string fullName)
        {
            ViewBag.Message = "Xin chào " + fullName;
            return View();
        }
    }
}