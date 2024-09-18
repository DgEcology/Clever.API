using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clever.Domain.Entities
{
    [Table("Reactions")]
    public class Reaction
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
       [Required]
        public Event? Event { get; set; }

        [Required]
        public User? User { get; set; }        
    }
}