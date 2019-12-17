using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class WorkEntry
    {
        [Key]
        public int Id { get; set; }
        // public TimeSpan AmountOfTime { get => End - Start; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public int WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
