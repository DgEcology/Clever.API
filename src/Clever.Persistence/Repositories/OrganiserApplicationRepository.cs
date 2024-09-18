using Clever.Domain.Entities;
using Clever.Domain.Interfaces;

namespace Clever.Persistence.Repositories
{
    public class OrganiserApplicationRepository : Repository<OrganiserApplication>, IOrganiserApplicationRepository
    {
        public OrganiserApplicationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {}
    }
}