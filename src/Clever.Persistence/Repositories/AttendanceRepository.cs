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
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AttendanceRepository(ApplicationDbContext applicationDbContext){
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Attendance>> GetAllAsync() {
            return await _applicationDbContext.Attendances.ToListAsync();
        }
        public async Task<Attendance> GetByIdAsync(long id){
            var attendance = await _applicationDbContext.Attendances.FirstOrDefaultAsync(x => x.Id == id);

            if (attendance is null){
                throw new NotFoundException(typeof(Attendance).Name, id);
            }

            return attendance;
        }

        public void Add (Attendance attendance){
            _applicationDbContext.Attendances.Add(attendance);
        }
    }
}