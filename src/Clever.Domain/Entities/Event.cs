using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public long Id {get; set;}

        [Required]
        public string Description {get; set;} 

        [Required]
        public string Image {get; set;}

        [Required]
        public string Geolocation {get; set;}

        [Required]
        public DateTime StartTime {get; set;}
        
        [Required]
        public DateTime EndTime {get; set;}

        [Required]
        public DateTime PublishTame {get; set;}

        [Required]
        public bool IsArchived {get; set;}

        [Required]
        public string SecretKey {get; set;}

        [Required]
        public Type? Type {get; set;}
        
        [Required]
        public User? User {get; set;}

        [Required]
        public Reaction? Reaction {get; set;}

        [Required]
        public Attendance? Attendance {get; set;}

    }
}