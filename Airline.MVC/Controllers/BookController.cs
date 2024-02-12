using Airline.Business.Services.Implementations;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace Airline.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Ticket user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //AppUser user1 = new AppUser()
            //{
            //    user1.Email=user.Ema
            //} 
            //SendEmailService.SendEmail(to: user.Email, name: user.Name);

            return View();
        }
    }
}
