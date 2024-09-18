using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;

namespace Clever.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TagRepository(ApplicationDbContext applicationDbContext){
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Tag>> GetAllAsync(){
            return await _applicationDbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(long id){
            var tag = await _applicationDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if(tag is null){
                throw new NotFoundException(typeof(Tag).Name, id);
            }

            return tag;
        }

        public void Add(Tag tag){
            _applicationDbContext.Tags.Add(tag);

            _applicationDbContext.SaveChanges();
        }
    }
}