using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal HourlyWage { get; set; }
        [Required]
        public int TotalAnnualLeave { get; set; }
        [Required]
        public int RemainingAnnualLeave { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkingGroupId { get; set; }
        public WorkingGroup WorkingGroup { get; set; }
    }
}
