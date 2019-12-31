using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.WorkingGroups
{
    public class WorkingGroupRepository : IWorkingGroupRepository
    {
        private AppDbContext _context;
        public WorkingGroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(WorkingGroup toAdd)
        {
            _context.WorkingGroups.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.WorkingGroups.FirstOrDefault(w => w.Id == id);
            return result != null ? true : false;
        }

        public async Task<IEnumerable<WorkingGroup>> GetAllAsync()
        {
             var workingGroups = await _context.WorkingGroups
                .Include(w => w.WorkingGroupSchedules)
                    .ThenInclude(w => w.WorkingDays)
                        .ThenInclude(w => w.WorkEntries)
                            .ThenInclude(w => w.WorkingAvailability)
                                .ThenInclude(w => w.User)
                .Include(w => w.Users)
                .ToListAsync();

            return workingGroups;
        }

        public async Task<WorkingGroup> GetById(int id)
        {
            var workingGroup = await _context.WorkingGroups
                .Include(w => w.WorkingGroupSchedules)
                    .ThenInclude(w => w.WorkingDays)
                        .ThenInclude(w => w.WorkEntries)
                .Include(w => w.Users)
                .FirstOrDefaultAsync(w => w.Id == id);

            return workingGroup;
        }

        public Task<WorkingGroup> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, WorkingGroup newValue)
        {
            throw new NotImplementedException();
        }
    }
}
