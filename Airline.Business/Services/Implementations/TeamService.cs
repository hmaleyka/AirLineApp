using Airline.Business.Exceptions;
using Airline.Business.Exceptions.Common;
using Airline.Business.Helpers;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.TeamVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repo;
        private readonly IWebHostEnvironment _env;

        public TeamService(ITeamRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<Team> Create(CreateTeamVM team)
        {
            if (team == null) throw new NotFoundException("It should not be null");
            Team teams = new Team()
            {
                Fullname = team.Fullname,
                Position = team.Position,
                Description = team.Description,
                AboutMe = team.AboutMe,
                number = team.number,
                email = team.email,
                Experience=team.Experience
            };

            if (!team.Image.CheckType("image/"))
            {
                throw new ImageException("Image type should be img" , nameof(team.Image));
            }
            if (!team.Image.CheckLong(2097152))
            {
                throw new ImageException("Image size should not be large than 2mb", nameof(team.Image));
            }
            teams.ImgUrl = team.Image.Upload(_env.WebRootPath, @"\Upload\Team\");

            await _repo.Create(teams);
            await _repo.SaveChangesAsync();
            return teams;
            
        }

        public async Task<Team> Delete(int id)
        {
            var team = await _repo.GetByIdAsync(id);
            if(team==null) throw new NotFoundException("it should not be null");
            team.IsDeleted = true;
            await _repo.SaveChangesAsync();
            return team;

        }

        public async Task<ICollection<Team>> GetAllAsync()
        {
            var teams = _repo.GetQuery(x => x.IsDeleted == false);
            return await teams.ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id shouldn't be less than or equal zero");
            var team = await _repo.GetByIdAsync(id);
            if (team == null) throw new NotFoundException("Id should not be null!");
            return team;
        }

        public async Task<Team> Update(UpdateTeamVM team)
        {
            Team teams = await _repo.GetByIdAsync(team.Id);
            if (teams == null) throw new NotFoundException("it should not be null");
            teams.Fullname = team.Fullname;
               teams.Position = team.Position;
               teams.Description = team.Description;
               teams.AboutMe = team.AboutMe;
                teams.number = team.number;
                teams.email = team.email;
            teams.Experience= team.Experience;

            if (!team.Image.CheckType("image/"))
            {
                throw new ImageException("Image type should be img", nameof(team.Image));
            }
            if (!team.Image.CheckLong(2097152))
            {
                throw new ImageException("Image size should not be large than 2mb", nameof(team.Image));
            }
            teams.ImgUrl = team.Image.Upload(_env.WebRootPath, @"\Upload\Team\");

            _repo.Update(teams);
            await _repo.SaveChangesAsync();
            return teams;
        }
    }
}
