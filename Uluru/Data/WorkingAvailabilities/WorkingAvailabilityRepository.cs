using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.WorkingAvailabilities
{
    public class WorkingAvailabilityRepository : IWorkingAvailabilityRepository
    {
        private AppDbContext _context;

        public WorkingAvailabilityRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(WorkingAvailability toAdd)
        {
            _context.WorkingAvailabilities.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.WorkingAvailabilities.FirstOrDefault(w => w.Id == id);
            return result != null ? true : false;
        }

        public async Task<IEnumerable<WorkingAvailability>> GetAllAsync()
        {
            var result = await _context.WorkingAvailabilities
                .ToListAsync();

            return result;
        }

        public async Task<WorkingAvailability> GetById(int id)
        {
            var result = await _context.WorkingAvailabilities
                .FirstOrDefaultAsync(w => w.Id == id);

            return result;
        }
        
        public async Task<IEnumerable<WorkingAvailability>> GetAllOfUserAsync(int id)
        {
            var result = await _context.WorkingAvailabilities
                .Where(w => w.UserId == id)
                .ToListAsync();

            return result;
        }

       

        public async Task<WorkingAvailability> Remove(int id)
        {
            var toRemove = _context.WorkingAvailabilities.FirstOrDefault(w => w.Id == id);
            if (toRemove == null)
                return null;

            _context.WorkingAvailabilities.Remove(toRemove);

            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task Update(int id, WorkingAvailability newValue)
        {
            if (!this.Exists(id))
                throw new KeyNotFoundException();
            var entity = await _context.WorkingAvailabilities.FirstOrDefaultAsync(w => w.Id == id);
            entity = newValue;
            _context.WorkingAvailabilities.Update(entity);
        }
    }
}

