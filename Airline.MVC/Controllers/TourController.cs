using Airline.Business.ViewModel;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.MVC.Controllers
{
    public class TourController : Controller
    {

        private readonly AppDbContext _context;

        public TourController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index ()
        {
            List<Package> packages = _context.packages.Where(p=>p.IsDeleted==false).ToList();
            return View(packages);
        }

        public IActionResult Detail(int id)
        {
            if (id == null) return BadRequest();

            Package package = _context.packages
                .Where(p=>p.IsDeleted == false)
                .FirstOrDefault(package =>package.Id == id);

            if(package == null) return NotFound();

            DetailVM details = new DetailVM()
            {
                package = package,
                packages = _context.packages.Where(p => p.Id != package.Id).ToList(),
            };

            return View(details);
        }
    }
}
