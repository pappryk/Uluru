using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class WorkingGroupSchedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public List<WorkEntry> WorkEntries { get; set; }
        public int WorkingGroupId { get; set; }
        public WorkingGroup WorkingGroup { get; set; }
    }
}
