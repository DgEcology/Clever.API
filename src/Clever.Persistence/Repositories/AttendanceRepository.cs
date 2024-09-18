using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clever.Domain.Interfaces;
using Clever.Domain.Entities;
using Clever.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Clever.Persistence.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}
        public async Task<List<Attendance>> GetByEventIdAsync(long eventId)
        {
            return await dbSet.Where(x => x.EventId == eventId).ToListAsync();
        }
        public async Task MarkAsAttended(long eventId, string userId)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId)
                ?? throw new NotFoundException(typeof(Attendance).Name);
            entity.Status = "Attended";
            dbSet.Update(entity);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}