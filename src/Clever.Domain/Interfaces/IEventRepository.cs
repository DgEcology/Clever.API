using System.Collections.Generic;
using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        public Task Archive(long id);
        public Task Accept(long id);
        public Task<Event> GetBySecretKeyAsync(string secreyKey);
        public Task<List<Event>> GetByTagAsync(long tagId);
    }
}