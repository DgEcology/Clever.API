using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.FileProviders;

namespace Clever.Web.DTO
{
    public record OrganisationDTO
    {
        [Required]
        public string OrganisationName { get; set; }

        [Required]
        public int OrganisationNumber { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public IFormFile Photo { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}