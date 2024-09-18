using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        public long Id { get; set; }

        public Event? Event { get; set; }

        public User? User { get; set; }
    }
}