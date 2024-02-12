using Airline.Business.Exceptions;
using Airline.Business.Services.Implementations;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            try
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<Ticket> ticket = _context.tickets.Where(t => t.user.Id == userId).ToList();
                var user = _context.Users.Find(userId);
                //Ticket tickets = _context.tickets.Where;
                if (user != null)
                {
                    SendBookEmailService.SendEmail(to: user.Email, name: user.Name);
                    return View(ticket);
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
        public IActionResult Book(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ticket = _context.tickets.Where(t => t.user.Id == userId).ToList();
            SendBookEmailService.SendEmail(to: user.Email, name: user.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}
