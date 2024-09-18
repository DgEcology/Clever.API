using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clever.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task Add(T entity);
        public Task<T> GetByIdAsync(long id);
        public Task<List<T>> GetAllAsync();
    }
}