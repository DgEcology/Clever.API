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
        [Required]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}