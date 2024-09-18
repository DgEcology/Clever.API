using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
    }
}