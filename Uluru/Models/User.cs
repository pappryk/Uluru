using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.Users.DTOs;

namespace Uluru.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal HourlyWage { get; set; }
        [Required]
        public int WorkingGroupId { get; set; }
        public WorkingGroup WorkingGroup { get; set; }

        public List<WorkEntry> WorkEntries { get; set; }

        public User()
        {
        }

        public User(UserRegistrationDTO dto)
        {
            Email = dto.Email;
            PasswordHash = dto.Password;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
        }
    }
}
