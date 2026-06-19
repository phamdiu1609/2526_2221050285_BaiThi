using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Default
        public string Default()
        {
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}