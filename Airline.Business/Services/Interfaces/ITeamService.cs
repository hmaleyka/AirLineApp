using Airline.Business.ViewModel.PackageVM;
using Airline.Business.ViewModel.TeamVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface ITeamService
    {
        Task<ICollection<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task<Team> Create(CreateTeamVM team);
        Task<Team> Update(UpdateTeamVM team);

        Task<Team> Delete(int id);
    }
}
