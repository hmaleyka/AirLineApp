using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.FlightVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FlightController : Controller
    {
        private readonly IFlightService _service;
        private readonly AppDbContext _context;

        public FlightController(IFlightService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var flights = await _service.GetAllAsync();
            return View(flights);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightVM flightvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Create(flightvm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details (int id)
        {
            Flight flight = await _context.flights.FindAsync(id);
            return View(flight);
        }
        //[HttpPost]
        //public async Task<IActionResult> Details ()
    }
}
