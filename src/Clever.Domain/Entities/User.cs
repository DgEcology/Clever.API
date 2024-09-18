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
    }
}