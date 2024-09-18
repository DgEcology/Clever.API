using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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