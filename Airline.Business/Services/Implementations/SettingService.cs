using Airline.Business.Exceptions.Common;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.SettingVM;
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
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _repo;

        public SettingService(ISettingRepository repo)
        {
            _repo = repo;
        }

        public async Task<Setting> Delete(int id)
        {
            var settings = await _repo.GetByIdAsync(id);
            if (settings == null)  throw new Exception("It should not be null");
             settings.IsDeleted = true;
            await _repo.SaveChangesAsync();
            return settings;
        }

        public async Task<ICollection<Setting>> GetAllAsync()
        {
            var settings = await _repo.GetAllAsync();
            return await settings.ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id should not be less than zero");
            var settings = await _repo.GetByIdAsync(id);
            if (settings == null) throw new NotFoundException("id should not be null");
            return settings;
        }

        public async Task<Setting> Update(UpdateSettingVM setting)
        {
            Setting settings = await _repo.GetByIdAsync(setting.Id);
            if(settings == null) throw new NotFoundException("id should not be null");
            settings.Key = setting.Key;
            settings.Value = setting.Value;
            _repo.Update(settings);
            await _repo.SaveChangesAsync();
            return settings;
        }
    }
}
