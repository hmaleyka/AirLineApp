﻿
using Airline.Business.ViewModel;
using Airline.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Airline.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM()
            {
                benefits = _context.benefits.Where(b=>b.IsDeleted==false).ToList(),
                packages= _context.packages.Where(b => b.IsDeleted == false).ToList(),
                teams = _context.teams.Where(b => b.IsDeleted == false).ToList(),
                deals= _context.deals.Include(d=>d.dealphotos).Where(b=>b.IsDeleted==false).ToList(),
            };
            return View(home);
        }

    }
}