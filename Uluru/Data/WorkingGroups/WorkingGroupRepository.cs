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
            _context.WorkingGroup.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkingGroup>> GetAllAsync()
        {
             var workingGroups = await _context.WorkingGroup
                .Include(w => w.WorkingGroupSchedules)
                .Include(w => w.Users)
                .ToListAsync();

            return workingGroups;
        }

        public async Task<WorkingGroup> GetById(int id)
        {
            var workingGroup = await _context.WorkingGroup
                .Include(w => w.WorkingGroupSchedules)
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
