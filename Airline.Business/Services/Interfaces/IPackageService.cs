using Airline.Business.ViewModel.BenefitVM;
using Airline.Business.ViewModel.PackageVM;
using Airline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Interfaces
{
    public interface IPackageService
    {
        Task<ICollection<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<Package> Create(PackageCreateVM package);
        Task<Package> Update(PackageUpdateVM package);

        Task<Package> Delete(int id);
    }
}
