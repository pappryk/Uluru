using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public int WorkingGroupId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
