using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        public long Id { get; set; }

        public long EventId { get; set; }

        public string UserId { get; set; }

        public string Status { get; set; }

        public Event? Event { get; set; }

        public User? User { get; set; }
    }
}