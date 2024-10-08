using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public long Id { get; set; }

        public string Description { get; set; } 

        public string Title { get; set; }

        public string Image { get; set; }

        public string Geolocation { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime PublishTime { get; set; }

        public bool IsArchived { get; set; }

        public bool IsAccepted { get; set; }

        public string SecretKey { get; set; }

        public long TagId { get; set; }

        public Tag? Tag { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Reaction>? Reactions { get; set; }

        public ICollection<Attendance>? Attendances { get; set; }

    }
}