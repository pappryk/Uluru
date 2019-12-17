using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.Users.DTOs;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.Users
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!this.Exists(user.Id))
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetAllUsersOfGroupAsync(int groupId)
        {
            var users = await _context.Users
                .Where(u => u.WorkingGroupId == groupId)
                .ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> Remove(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task Update(int id, User newValue)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public bool UserWithEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async Task<bool> Authenticate(UserAuthenticationDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

            if (user == null)
                return false;
            if (user.PasswordHash != userDto.Password)
                return false;

            return true;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
