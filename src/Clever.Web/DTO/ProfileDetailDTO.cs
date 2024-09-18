using System.ComponentModel.DataAnnotations;

namespace Clever.Web.DTO
{
    public class ProfileDetailDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public int Age { get; set; }
    }
}