using System.Collections.Generic;
using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IReactionRepository : IRepositoryBase<Reaction>
    {
        public Task<List<Reaction>> GetByEventIdAsync(long eventId);
        public Task Remove(long id);
    }
}