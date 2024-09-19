using System.Threading.Tasks;
using Clever.Domain.Entities;

namespace Clever.Domain.Interfaces
{
    public interface IOrganiserApplicationRepository : IRepositoryBase<OrganiserApplication>
    {
        public Task Accept(long id);
    }
}