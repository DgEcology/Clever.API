using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Interfaces;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;

namespace Clever.Persistence.Repositories;
public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}
    public new async Task<List<Event>> GetAllAsync()
    {
        return await dbSet.Where(x => x.IsAccepted).ToListAsync();
    }
    public async Task Archive(long id)
    {
        var eventEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(typeof(Event).Name, id);
        eventEntity.IsArchived = true;
        dbSet.Update(eventEntity);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task Accept(long id)
    {
        var eventEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(typeof(Event).Name, id);
        eventEntity.IsAccepted = true;
        dbSet.Update(eventEntity);
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task<List<Event>> GetByTagAsync(long tagId)
    {
        return await dbSet.Where(x => x.IsAccepted && x.TagId == tagId).ToListAsync();
    }

    public async Task<Event> GetBySecretKeyAsync(string secretKey)
    {
        var eventEntity = await dbSet.FirstOrDefaultAsync(x => x.SecretKey == secretKey) ?? throw new NotFoundException(typeof(Event).Name);
        return eventEntity;
    }
}