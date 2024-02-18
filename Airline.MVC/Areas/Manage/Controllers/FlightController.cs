using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.FlightVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [AutoValidateAntiforgeryToken]
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
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Index()
        {
            var flights = await _service.GetAllAsync();
            return View(flights);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
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
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Details(int id)
        {
            Flight flight = await _context.flights.FindAsync(id);
            return View(flight);
        }
        public async Task<IActionResult> Update(int id)
        {
            var flight = await _service.GetByIdAsync(id);
            UpdateFlightVM flightvm = new UpdateFlightVM()
            {
                Seat = flight.Seat,
                From = flight.From,
                To = flight.To,
                flightdate = flight.flightdate,
                People = flight.People,
                Price = flight.Price,
            };
            return View(flightvm);
        }

        [HttpPost]
        public async Task<IActionResult> Update (UpdateFlightVM flightvm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var flight = await _service.Update(flightvm);
            return RedirectToAction(nameof(Index));
        }
    }
}
