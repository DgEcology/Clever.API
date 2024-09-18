using System.Collections.Generic;
using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IAttendanceRepository : IRepositoryBase<Attendance>
    {
        public Task<List<Attendance>> GetByEventIdAsync(long eventId);
        public Task MarkAsAttended(long eventId, string userId);
    }
}