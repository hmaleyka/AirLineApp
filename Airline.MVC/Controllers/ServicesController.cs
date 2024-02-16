using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           List<Benefit> benefits = _context.benefits.Where(b=>b.IsDeleted==false).ToList();
            return View(benefits);
        }
    }
}
