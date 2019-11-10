using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Data.Users.DTOs
{
    public class UserAuthenticationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
