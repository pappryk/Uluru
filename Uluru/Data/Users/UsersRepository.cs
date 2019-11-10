using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.Users
{
    public class UsersRepository : IUsersRepository
    {
        private AppDbContext _context;
        public UsersRepository(AppDbContext context)
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
                if (!this.UserExists(user.Id))
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
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

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public bool UserWithEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
