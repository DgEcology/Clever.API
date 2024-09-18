using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clever.Domain.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}