using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        public Task Archive(long id);
    }
}