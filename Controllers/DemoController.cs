using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Text.Encodings.Web;
namespace MVC.Controllers
{
    public class DemoController : Controller
    { 
        // GET: /Demo/
        public IActionResult Index()
        {
            return View();
        }
        
        
        // GET: /Demo/Welcome/ 

        public string Welcome()
        {
            return "Hello! Phạm Thị Dịu -2221050285";
        }
    }
}