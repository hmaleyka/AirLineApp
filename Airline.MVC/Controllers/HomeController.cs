
using Airline.Business.ViewModel;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Airline.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM()
            {
                benefits = _context.benefits.ToList(),
                packages= _context.packages.ToList(),
            };
            return View(home);
        }

    }
}