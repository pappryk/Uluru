using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.Users.DTOs
{
    public class UserRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal HourlyWage { get; set; }
        public UserRole UserRole { get; set; }
        public int WorkingGroupId { get; set; }
        public int? PositionId { get; set; }
    }
}
