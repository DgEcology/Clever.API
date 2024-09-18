using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IReactionRepository : IRepositoryBase<Reaction>
    {
        public void Remove(long id);
        public Task<List<Reaction>> GetByEventIdAsync(long eventId);
    }
}