using Airline.Business.Exceptions.Common;
using Airline.Business.Helpers;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel.DealVM;
using Airline.Core.Entities;
using Airline.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Business.Services.Implementations
{
    public class DealService : IDealService
    {
        private readonly IDealRepository _repo;
        private readonly IWebHostEnvironment _env;


        public DealService(IDealRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<Deal> Create(DealCreateVM dealvm)
        {
            if (dealvm == null) throw new NotFoundException("It should not be null");
            Deal deals = new Deal()
            {
                Title = dealvm.Title,
                Description = dealvm.Description,
                Feature = dealvm.Feature,
                Distance = dealvm.Distance,
                Price = dealvm.Price,
                Range = dealvm.Range,
                Speed = dealvm.Speed,
                Passenger = dealvm.Passenger,
                dealphotos = new List<DealPhoto>()
            };
            if (!dealvm.MainPhoto.CheckType("image/"))
            {
                throw new Exception("Image type should be img");
            }
            if (!dealvm.MainPhoto.CheckLong(2097152))
            {
                throw new Exception("Image size should not be large than 2mb");
            }
            deals.MainPhoto = dealvm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Deal\");
            if (dealvm.dealphotos != null)
            {
                foreach (var photo in dealvm.dealphotos)
                {
                    if (!photo.CheckType("image/"))
                    {
                        throw new Exception("Image type should be img");
                    }
                    if (!photo.CheckLong(2097152))
                    {
                        throw new Exception("Image size should not be large than 2mb");
                    }
                    DealPhoto dealphoto = new DealPhoto()
                    {
                        ImgUrl = photo.Upload(_env.WebRootPath, @"\Upload\Deal\"),
                        deal = deals
                    };
                    deals.dealphotos.Add(dealphoto);

                }
            }
            await _repo.Create(deals);
            await _repo.SaveChangesAsync();
            return deals;

        }

        public async Task<Deal> Delete(int id)
        {
            var deal = await _repo.GetByIdAsync(id);
            if (deal == null) throw new NotFoundException("It should not be null");
            //_repo.Delete(deal);
            deal.IsDeleted = true;
            await _repo.SaveChangesAsync();
            return deal;
        }

        public async Task<ICollection<Deal>> GetAllAsync()
        {
            var deals = _repo.GetQuery(x => x.IsDeleted == false)
            .Include(x => x.dealphotos).ToListAsync();
            return await deals;
        }

        public async Task<Deal> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id should not be less than zero");
            var deal = await _repo.GetQuery(x => x.IsDeleted == false && x.Id == id)
                .Include(x => x.dealphotos).FirstOrDefaultAsync();
            if (deal == null) throw new NotFoundException("id should not be null");
            return deal;
        }

        public async Task<Deal> Update(DealUpdateVM dealvm)
        {
            Deal existdeal = await _repo.GetQuery(x => x.IsDeleted == false && x.Id == dealvm.Id).Include(d=>d.dealphotos).FirstOrDefaultAsync();
            if (existdeal == null) throw new NotFoundException("Id should not be null!");
            existdeal.Title = dealvm.Title;
            existdeal.Description = dealvm.Description;
            existdeal.Feature = dealvm.Feature;
            existdeal.Distance = dealvm.Distance;
            existdeal.Range = dealvm.Range;
            existdeal.Speed = dealvm.Speed;
            existdeal.Passenger = dealvm.Passenger;
            existdeal.Price = dealvm.Price;
           // existdeal.MainPhoto = dealvm.MainphotoUrl;
            
            
            if (dealvm.MainPhoto != null)
            {

                if (!dealvm.MainPhoto.CheckType("image/"))
                {
                    throw new Exception("Image type should be img");
                }
                if (!dealvm.MainPhoto.CheckLong(2097152))
                {
                    throw new Exception("Image size should not be large than 2mb");
                }
                //var oldphoto = existdeal.dealphotos.FirstOrDefault();
                //existdeal.MainPhoto.Remove(oldphoto);
                existdeal.MainPhoto = dealvm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Deal\");
                
            }

            if (dealvm.ImageIds == null)
            {
                existdeal.dealphotos.RemoveAll(d=>d.IsDeleted=false);
            }
            else
            {
                var removeListImage = existdeal.dealphotos?.Where(p => !dealvm.ImageIds.Contains(p.Id)).ToList();
                if (removeListImage != null)
                {
                    foreach (var image in removeListImage)
                    {
                        existdeal.dealphotos.Remove(image);
                        FileManager.DeleteFile(image.ImgUrl, _env.WebRootPath, @"\Upload\Deal\");
                    }

                }
                else
                {
                    existdeal.dealphotos.RemoveAll(p =>p.IsDeleted=false);
                }

            }




            if (dealvm.dealphotos != null)
            {
                foreach (var photo in dealvm.dealphotos)
                {
                    if (!photo.CheckType("image/"))
                    {
                        throw new Exception("Image type should be img");
                    }
                    if (!photo.CheckLong(2097152))
                    {
                        throw new Exception("Image size should not be large than 2mb");
                    }
                    DealPhoto multiplephotos = new DealPhoto()
                    {
                        ImgUrl = photo.Upload(_env.WebRootPath, @"\Upload\Deal\"),
                        deal = existdeal,
                    };
                    existdeal.dealphotos.Add(multiplephotos);
                }
            }   
            _repo.Update(existdeal);
            await _repo.SaveChangesAsync();
            return existdeal;




        }
    }
}
