using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("OrganiserApplications")]
    public class OrganiserApplication
    {
        [Key]
        public long Id { get; set; }

        public string Photo { get; set; }

        public string About { get; set; }

        public string OrganisationName { get; set; }

        public string OrganisationNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsAccepted { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }
    }
}