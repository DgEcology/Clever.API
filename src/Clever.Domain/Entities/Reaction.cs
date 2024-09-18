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
        public long Id { get; set; }

        public long EventId { get; set; }

        public string UserId { get; set; }
        
        public Event? Event { get; set; }

        public User? User { get; set; }        
    }
}