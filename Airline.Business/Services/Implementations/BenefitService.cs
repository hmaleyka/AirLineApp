using Airline.Business.Exceptions.Common;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.BenefitVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class BenefitService : IBenefitService
    {
        private readonly IBenefitRepository _repo;

        public BenefitService(IBenefitRepository repo)
        {
            _repo = repo;
        }

        public async Task<Benefit> Create(BenefitCreateVM benefit)
        {
            if (benefit == null) throw new NotFoundException("It should not be null");
            Benefit benefits = new Benefit()
            {
                Title = benefit.Title,
                Description = benefit.Description,
                Icon = benefit.Icon,
            };
            await _repo.Create(benefits);
            await _repo.SaveChangesAsync();
            return benefits;
        }

        public async Task<Benefit> Delete(int id)
        {
            var benefit = await _repo.GetByIdAsync(id);
            if (benefit == null) throw new NotFoundException("Id should not be null");
            benefit.IsDeleted = true;
            await _repo.SaveChangesAsync();
            return benefit;
        }

        public async Task<ICollection<Benefit>> GetAllAsync()
        {
            var benefits = await _repo.GetAllAsync();
            return await benefits.ToListAsync();
        }

        public async Task<Benefit> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id shouldn't be less than or equal zero");
            var benefit = await _repo.GetByIdAsync(id);
            if (benefit == null) throw new NotFoundException("Id should not be null!");
            return benefit;
        }

        public async Task<Benefit> Update(BenefitUpdateVM benefit)
        {
            Benefit benefits = await _repo.GetByIdAsync(benefit.Id);
            if (benefits == null) throw new NotFoundException("Id should not be null!");
            benefits.Title = benefit.Title;
            benefits.Description = benefit.Description;
            benefits.Icon = benefit.Icon;
            _repo.Update(benefits);
            await _repo.SaveChangesAsync();
            return benefits;
        }
    }
}
