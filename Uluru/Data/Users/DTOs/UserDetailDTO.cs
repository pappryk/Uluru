using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.Users.DTOs
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal HourlyWage { get; set; }
        public string UserRole { get; set; }
        public int WorkingGroupId { get; set; }
        public int? PositionId { get; set; }


        public UserDetailDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            HourlyWage = user.HourlyWage;
            UserRole = user.UserRole.ToString();
            WorkingGroupId = user.WorkingGroupId;
            PositionId = user.PositionId;
        }
    }
}
