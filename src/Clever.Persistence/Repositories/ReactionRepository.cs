using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;

namespace Clever.Persistence.Repositories
{
    public class ReactionRepository : Repository<Reaction>, IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}
        public async Task<List<Reaction>> GetByEventIdAsync(long eventId)
        {
            return await dbSet.Where(x => x.EventId == eventId).ToListAsync();
        }
        public async Task Remove(long id)
        {
            var reaction = dbSet.FirstOrDefault(x => x.Id == id) ?? throw new NotFoundException(typeof(Reaction).Name, id);
            dbSet.Remove(reaction);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}