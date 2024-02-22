using Airline.Business.Exceptions;
using Airline.Business.Services.Implementations;
using Airline.Business.ViewModel;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Security.Claims;
using static QRCoder.PayloadGenerator;

namespace Airline.MVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;
        //private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            //int flightId;
            //var flight = _context.flights.FirstOrDefault(f => f.Id == flightId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Ticket> tickets = _context.tickets.Include(t => t.flight).Where(t => t.user.Id == userId).ToList();
            var user = _context.Users.Find(userId);
            //Flight flight = _context.flights.FirstOrDefault(f => f.Id == flightId);
            //Ticket ticket = new Ticket()
            //{
            //    userId = userId,
            //    FlightId = flightId,
            //    seat = 1,

            //};
            //_context.Add(ticket);
            //_context.SaveChanges();
            return View(tickets);

        }
        public IActionResult Book(int flightId)
        {
            try
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //List<Ticket> tickets = _context.tickets.Where(t => t.user.Id == userId).ToList();
                var user = _context.Users.Find(userId);
                //Ticket tickets = _context.tickets.Where;
                if (user != null)
                {
                    SendBookEmailService.SendEmail(to: user.Email, name: user.Name);
                    var flightExists = _context.flights.Any(f => f.Id == flightId);
                    Ticket ticket = new Ticket()
                    {
                        userId = userId,
                        FlightId = flightId,
                        seat = 1,

                    };
                    _context.Add(ticket);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Ticket");
                }
                else
                {
                    throw new UserNotFoundException("You should firstly login the website", nameof(userId));
                }
                //return View(tickets);
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.name, ex.Message);
                return View("Error");

            }

        }

      
    }
}
