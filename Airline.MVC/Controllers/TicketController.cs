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

        public IActionResult BarCode()
        {
            QRCodeVM model = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult BarCode(QRCodeVM model)
        {
            Payload? payload = null;
            switch (model.QRCodeType)
            {

                case 1: 
                    payload = new SMS(model.SMSPhoneNumber, model.SMSBody);
                    break;
                case 2: 
                    payload = new WhatsAppMessage(model.WhatsAppNumber, model.WhatsAppMessage);
                    break;
                case 3:
                    payload = new Mail(model.ReceiverEmailAddress, model.EmailSubject, model.EmailMessage);
                    break;
                case 4: 
                    payload = new WiFi(model.WIFIName, model.WIFIPassword, WiFi.Authentication.WPA);
                    break;

            }

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload);
            BitmapByteQRCode qrCode = new(qrCodeData);
            string base64String = Convert.ToBase64String(qrCode.GetGraphic(20));
            model.QRImageURL = "data:image/png;base64," + base64String;
            return View("BarCode", model);
        }
    }
}
