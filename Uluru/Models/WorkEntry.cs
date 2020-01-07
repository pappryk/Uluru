using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class WorkEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public int WorkingGroupScheduleId { get; set; }
        public WorkingGroupSchedule WorkingGroupSchedule { get; set; }
        public int? WorkingAvailabilityId { get; set; }
        public WorkingAvailability WorkingAvailability { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
