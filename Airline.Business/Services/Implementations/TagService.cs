using Airline.Business.Exceptions.Common;
using Airline.Business.Services.Interfaces;
using Airline.Business.ViewModel;
using Airline.Business.ViewModel.TagVM;
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
    public class TagService : ITagService
    {
        private readonly ITagRepository _repo;

        public TagService(ITagRepository repo)
        {
            _repo = repo;
        }

        public async Task<Tag> Create(CreateTagVM tagvm)
        {
            if (tagvm == null) throw new NotFoundException("It should not be null");
            Tag tags = new Tag()
            {
                Name = tagvm.Name,

            };
            await _repo.Create(tags);
            await _repo.SaveChangesAsync();
            return tags;
        }

        public async Task<Tag> Delete(int id)
        {
            var tags = await _repo.GetByIdAsync(id);
            if (tags == null) throw new NotFoundException("It should not be null");
            tags.IsDeleted= true;
            await _repo.SaveChangesAsync();
            return tags;
        }

        public async Task<ICollection<Tag>> GetAllAsync()
        {
            var tags = _repo.GetQuery(t=>t.IsDeleted==false).Include(t=>t.blogtags).ThenInclude(t=>t.blog);
            return await tags.ToListAsync();

        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Id shouldn't be less than or equal zero");
            Tag tag = await _repo.GetByIdAsync( id);
            if (tag == null) throw new NotFoundException("It should not be null");
            return tag; 

        }

        public async Task<Tag> Update(UpdateTagVM tagvm)
        { 
            Tag existtags = await _repo.GetQuery(x => x.IsDeleted == false && x.Id == tagvm.Id).Include(t=>t.blogtags).ThenInclude(t=>t.blog).FirstOrDefaultAsync();
            if(existtags==null) throw new NotFoundException("Id should not be null!");
            existtags.Name= tagvm.Name;
            _repo.Update(existtags);
            await _repo.SaveChangesAsync();
            return existtags;
        }
    }
}
