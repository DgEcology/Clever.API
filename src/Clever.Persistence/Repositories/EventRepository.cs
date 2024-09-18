using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Interfaces;
using Clever.Domain.Entities;
using Clever.Domain;
using Clever.Domain.Exceptions;

namespace Clever.Persistence.Repositories;
public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public EventRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

    public async Task<List<Event>> GetAllAsync() => await _applicationDbContext.Events.ToListAsync();

    public async Task<Event> GetByIdAsync(long id)
    {
        var ev = await _applicationDbContext.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (ev is null) throw new NotFoundException(typeof(Event).Name, id);

        return ev;
    }
    public void Add(Event entity)
    {
        _applicationDbContext.Events.Add(entity);

        _applicationDbContext.SaveChanges();
    }

    public async Task Archive(long id)
    {
        var ev = await _applicationDbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

        if (ev is null) throw new NotFoundException(typeof(Event).Name, id);

        ev.IsArchived = true;

        _applicationDbContext.Events.Update(ev);

        await _applicationDbContext.SaveChangesAsync();
    }
}