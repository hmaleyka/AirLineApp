using Airline.Business.ViewModel;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.MVC.Controllers
{
    public class PlaneController : Controller
    {
        private readonly AppDbContext _context;

        public PlaneController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();

            Deal deals = _context.deals
                .Where(p => p.IsDeleted == false).Include(d=>d.dealphotos)
                .FirstOrDefault(deals => deals.Id == id);

            if (deals == null) return NotFound();

            DetailVM details = new DetailVM()
            {
                deal = deals,
                deals = _context.deals.Where(p => p.Id != deals.Id).ToList(),
            };

            return View(details);
        }
    }
}
