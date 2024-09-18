using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Domain.Entities
{
    [Table("OrganiserApplications")]
    public class OrganiserApplication
    {
        [Key]
        public long Id { get; set; }

        public string Photo { get; set; }

        public string AboutMe { get; set; }

        public string PassportData { get; set; }

        public bool IsAccepted { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }
    }
}