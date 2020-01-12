using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.WorkingGroupSchedules
{
    public class WorkingGroupScheduleRepository : IWorkingGroupScheduleRepository
    {
        private AppDbContext _context;
        
        public WorkingGroupScheduleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(WorkingGroupSchedule toAdd)
        {
            _context.WorkingGroupSchedules.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.WorkingGroupSchedules.FirstOrDefault(w => w.Id == id);
            return result != null ? true : false;
        }

        public async Task<IEnumerable<WorkingGroupSchedule>> GetAllAsync()
        {
            var result = await _context.WorkingGroupSchedules
                .Include(w => w.WorkEntries)
                    .ThenInclude(w => w.WorkingAvailability)
                        .ThenInclude(w => w.User)
                .Include(w => w.WorkEntries)
                    .ThenInclude(w => w.User)
                .Include(w => w.WorkingGroup)
                    .ThenInclude(w => w.Positions)
                .ToListAsync();

            return result;
        }

        public async Task<WorkingGroupSchedule> GetById(int id)
        {
            var result = await _context.WorkingGroupSchedules
                .Include(w => w.WorkEntries)
                        .ThenInclude(w => w.WorkingAvailability)
                            .ThenInclude(w => w.User)
                .Include(w => w.WorkEntries)
                        .ThenInclude(w => w.User)
                .Include(w => w.WorkingGroup)
                    .ThenInclude(w => w.Positions)
                .FirstOrDefaultAsync(w => w.Id == id);

            return result;
        }

        public async Task<WorkingGroupSchedule> Remove(int id)
        {
            var toRemove = _context.WorkingGroupSchedules.FirstOrDefault(w => w.Id == id);
            if (toRemove == null)
                return null;

            _context.WorkingGroupSchedules.Remove(toRemove);

            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task Update(int id, WorkingGroupSchedule newValue)
        {
            if (!this.Exists(id))
                throw new KeyNotFoundException();
            var entity = await _context.WorkingGroupSchedules.FirstOrDefaultAsync(w => w.Id == id);
            entity = newValue;
            _context.WorkingGroupSchedules.Update(entity);
        }
    }
}
