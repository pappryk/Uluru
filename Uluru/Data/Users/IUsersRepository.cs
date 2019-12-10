using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.Users.DTOs;
using Uluru.Models;

namespace Uluru.Data.Users
{
    public interface IUsersRepository : IRepository<User>
    {
        bool UserExists(int id);
        bool UserWithEmailExists(string email);
        Task<bool> Authenticate(UserAuthenticationDTO userDto);
        User GetByEmail(string email);
    }
}
