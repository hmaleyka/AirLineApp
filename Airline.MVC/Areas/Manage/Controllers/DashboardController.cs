using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
