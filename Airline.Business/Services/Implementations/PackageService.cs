using Airline.Business.Exceptions.Common;
using Airline.Business.Helpers;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.PackageVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _repo;
        private readonly IWebHostEnvironment _env;

        public PackageService(IPackageRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<Package> Create(PackageCreateVM packagevm )
        {
            if (packagevm == null) throw new NotFoundException("It should not be null");
            Package package = new Package()
            {
                Title = packagevm.Title,
                Description = packagevm.Description,
                Feature = packagevm.Feature,
                Perk = packagevm.Perk,
                Price = packagevm.Price,
                Duration = packagevm.Duration,
                date = packagevm.date,
                Person=packagevm.Person,
            };
            if (!packagevm.Image.CheckType("image/"))
            {
                throw new Exception("Image type should be img");
            }
            if (!packagevm.Image.CheckLong(2097152))
            {
                throw new Exception("Image size should not be large than 2mb");
            }
            package.ImgUrl = packagevm.Image.Upload(_env.WebRootPath, @"/Upload/Package/");

            await _repo.Create(package);
            await _repo.SaveChangesAsync();
            return package;
        }

        public async Task<Package> Delete(int id)
        {
            var package = await _repo.GetByIdAsync(id);
            if (package == null) throw new NotFoundException("It should not be null");
            _repo.Delete(package);
            await _repo.SaveChangesAsync();
            return package;

        }

        public async Task<ICollection<Package>> GetAllAsync()
        {
            var packages = await _repo.GetAllAsync();
            return await packages.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id should not be less than zero");
            var packages = await _repo.GetByIdAsync(id);
            if (packages == null) throw new NotFoundException("id should not be null");
            return packages;
        }

        public async Task<Package> Update(PackageUpdateVM packagevm)
        {
            Package package = await _repo.GetByIdAsync(packagevm.Id);
            if (package == null) throw new NotFoundException("id should not be null");
            package.Title= packagevm.Title;
            package.Description= packagevm.Description;
            package.Price = packagevm.Price;
            package.date = packagevm.date;
            package.Feature= packagevm.Feature;
            package.Perk= packagevm.Perk;
            package.Duration=packagevm.Duration;
            package.Person= packagevm.Person;

            if (!packagevm.Image.CheckType("image/"))
            {
                throw new Exception("Image type should be img");
            }
            if (!packagevm.Image.CheckLong(2097152))
            {
                throw new Exception("Image size should not be large than 2mb");
            }
            package.ImgUrl = packagevm.Image.Upload(_env.WebRootPath, @"/Upload/Package/");
            _repo.Update(package);
            await _repo.SaveChangesAsync();
            return package;



        }
    }
}
