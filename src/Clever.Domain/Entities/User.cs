using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Clever.Domain.Entities
{
    public class User : IdentityUser
    {
        public int Age { get; set; }

        public string? About { get; set; }

        public int Points { get; set; }

        // public string ProfilePicture { get; set; }

        public ICollection<Attendance> Attendance { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Reaction> Reactions { get; set; }

        public OrganiserApplication? OrganiserApplication { get; set; }
    }
}