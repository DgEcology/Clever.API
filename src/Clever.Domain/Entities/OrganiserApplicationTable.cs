using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Domain.Entities
{
    [Table("OrganiserApplicationTable")]
    public class OrganiserApplication
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string AboutMe {get; set; }

        [Required]
        public string PassportData {get; set; }

        [Required]
        public User? User { get; set; }
    }
}