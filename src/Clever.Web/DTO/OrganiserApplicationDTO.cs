using System.ComponentModel.DataAnnotations;

namespace Clever.Web.DTO
{
    public record OrganiserApplicationDTO
    {
        [Required]
        public string? OrganisationName { get; set; }

        [Required]
        public string? OrganisationNumber { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? About { get; set; }

        [Required]
        public IFormFile? Photo { get; set; }
    }
}