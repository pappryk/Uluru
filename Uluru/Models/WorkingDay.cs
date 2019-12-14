using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class WorkingDay
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkingGroupScheduleId { get; set; }
        public WorkingGroupSchedule WorkingGroupSchedule { get; set; }
        public List<WorkEntry> WorkEntries { get; set; }
    }
}
