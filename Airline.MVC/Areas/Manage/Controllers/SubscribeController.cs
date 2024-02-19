using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Subscribe> _table;
        public SubscribeController(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<Subscribe>();
        }
        [Authorize(Roles = "SuperAdmin, Admin , Moderator")]
        public IActionResult Index()
        {
            List<Subscribe> subscriptions = _table.ToList();
            return View(subscriptions);
        }
    }
}
