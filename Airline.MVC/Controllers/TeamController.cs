using Airline.Business.ViewModel;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.MVC.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Team> team = _context.teams.Where(b => b.IsDeleted == false).ToList();
            return View(team);
        }
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            Team team = _context.teams.FirstOrDefault(b => b.Id == id);

            if (team == null) return NotFound();
            DetailVM details = new DetailVM()
            {
                team = team,
                teams = _context.teams.Where(t => t.Id != team.Id).ToList()
            };
            return View(details);
        }
    }
}
