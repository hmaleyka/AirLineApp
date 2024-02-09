﻿using Airline.Business.Exceptions;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.TeamVM;
using Airline.Core.Entities;
using Airline.DAL.Context;
using Airline.MVC.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly ITeamService _service;
        private readonly AppDbContext _context;
        public TeamController(ITeamService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index(int page =1)
        {
            var query = _context.teams.AsQueryable();
            PaginatedList<Team> paginatedOrders = PaginatedList<Team>.Create(query, page, 3);
            //var teams = await _service.GetAllAsync();
            return View(paginatedOrders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamVM teamvm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _service.Create(teamvm);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {

                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
           
        }
        public async Task<IActionResult> Update(int id)
        {
            var team = await _service.GetByIdAsync(id);
            UpdateTeamVM teamvm = new UpdateTeamVM()
            {
                Fullname = team.Fullname,
                Position = team.Position,
                Description = team.Description,
                AboutMe = team.AboutMe,
                number = team.number,
                email = team.email,
                ImgUrl = team.ImgUrl,
            };
            return View(teamvm);
        }
        [HttpPost]
        public async Task<IActionResult> Update (UpdateTeamVM teamvm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var teams = await _service.Update(teamvm);
                return RedirectToAction(nameof(Index));
            }
            catch (ImageException ex)
            {

                ModelState.AddModelError(ex.name, ex.Message);

                return View();
            }
        
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
