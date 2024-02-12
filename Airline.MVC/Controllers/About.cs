using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Controllers
{
    public class About : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
