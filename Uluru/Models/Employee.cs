using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class Employee
    {
        [Required]
        public decimal HourlyWage { get; set; }
        [Required]
        public int RemainingAnnualLeave { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkingGroupId { get; set; }
        public WorkingGroup WorkingGroup { get; set; }
    }
}
