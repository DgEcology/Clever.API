using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Entities;
using Clever.Domain.Interfaces;
using Clever.Domain.Exceptions;

namespace Clever.Persistence.Repositories
{
    public class OrganiserApplicationRepository : IOrganiserApplicationTableRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrganiserApplicationRepository(ApplicationDbContext applicationDbContext){
            _applicationDbContext = applicationDbContext;
        }


        public async Task<List<OrganiserApplication>> GetAllAsync(){
            return await _applicationDbContext.OrganiserApplications.ToListAsync();
        }

        public async Task<OrganiserApplication> GetByIdAsync(long id)
        {
            var organiseApp = await _applicationDbContext.OrganiserApplications.FirstOrDefaultAsync(x => x.Id == id);
            if (organiseApp is null) throw new NotFoundException(typeof(OrganiserApplication).Name, id);
            return organiseApp;
        }

        public void Add(OrganiserApplication organiseApp){
            _applicationDbContext.OrganiserApplications.Add(organiseApp);

            _applicationDbContext.SaveChanges();
        }
    }
}