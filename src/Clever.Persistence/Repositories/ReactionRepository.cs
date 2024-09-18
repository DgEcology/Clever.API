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
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReactionRepository(ApplicationDbContext applicationDbContext){
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Reaction>> GetAllAsync(){
            return await _applicationDbContext.Reactions.ToListAsync();
        }

        public async Task<Reaction> GetByIdAsync(long id){
            var reaction = await _applicationDbContext.Reactions.FirstOrDefaultAsync();

            if (reaction is null)
                throw new NotFoundException(typeof(Reaction).Name, id);
            
            return reaction;
        }

        public void Add(Reaction reaction){
            _applicationDbContext.Reactions.Add(reaction);

            _applicationDbContext.SaveChanges();
        }

        public void Remove(long id){
            var reaction = _applicationDbContext.Reactions.FirstOrDefault(x => x.Id == id);

            if(reaction is null)
                throw new NotFoundException(typeof(Reaction).Name, id);
            
            _applicationDbContext.Reactions.Remove(reaction);

            _applicationDbContext.SaveChanges();
        }
    }
}