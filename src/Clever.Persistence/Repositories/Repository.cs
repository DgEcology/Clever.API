using System.Collections.Generic;
using System.Threading.Tasks;
using Clever.Domain.Exceptions;
using Clever.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clever.Persistence.Repositories;

public class Repository<T> : IRepositoryBase<T> where T : class
{
    protected readonly ApplicationDbContext applicationDbContext;
    protected readonly DbSet<T> dbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
        this.dbSet = applicationDbContext.Set<T>();
    }
    public async Task Add(T entity)
    {
        dbSet.Add(entity);
        await applicationDbContext.SaveChangesAsync();
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
    public async Task<T> GetByIdAsync(long id)
    {
        var entity = await dbSet.FindAsync(id) ?? throw new NotFoundException(typeof(T).Name, id);
        return entity;
    }
}