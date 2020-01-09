using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.Users.DTOs;
using Uluru.Models;

namespace Uluru.Data.Users
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserWithEmailExists(string email);
        Task<bool> Authenticate(UserAuthenticationDTO userDto);
        Task<User> GetByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersOfGroupAsync(int groupId);
        Task<User> UpdatePassword(int id, string oldPassword, string newPassword);
    }
}
