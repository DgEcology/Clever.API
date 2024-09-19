using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clever.Domain.Entities;
using Clever.Domain.Interfaces;
using Clever.Domain.Exceptions;

namespace Clever.Persistence.Repositories
{
    public class OrganiserApplicationRepository : Repository<OrganiserApplication>, IOrganiserApplicationRepository
    {
        public OrganiserApplicationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}

        public async Task Accept(long id)
        {
            var application = await dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(typeof(Event).Name, id);
            application.IsAccepted = true;
            dbSet.Update(application);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}