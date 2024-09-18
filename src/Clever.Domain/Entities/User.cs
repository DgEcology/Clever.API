using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Clever.Domain.Entities
{
    public class User : IdentityUser
    {
        public string AboutMe { get; set; }

        public int Points { get; set; }

        public int EventsOrganised { get; set; }

        public string Picture { get; set; }

        public string PassportData { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Reaction> Reactions { get; set; }

        public ICollection<OrganiserApplication> OrganiserApplications { get; set; }
    }
}