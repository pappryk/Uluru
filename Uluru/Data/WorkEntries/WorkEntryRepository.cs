using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.WorkEntries
{
    public class WorkEntryRepository : IWorkEntryRepository
    {
        private AppDbContext _context;

        public WorkEntryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(WorkEntry toAdd)
        {
            _context.WorkEntries.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public async Task AddMany(IEnumerable<WorkEntry> workEntries)
        {
            _context.WorkEntries.AddRange(workEntries); 
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.WorkEntries.FirstOrDefault(w => w.Id == id);
            return result != null ? true : false;
        }

        public async Task<IEnumerable<WorkEntry>> GetAllAsync()
        {
            var result = await _context.WorkEntries
                .ToListAsync();

            return result;
        }

        public async Task<WorkEntry> GetById(int id)
        {
            var result = await _context.WorkEntries
                .FirstOrDefaultAsync(w => w.Id == id);

            return result;
        }

        public async Task<WorkEntry> Remove(int id)
        {
            var toRemove = _context.WorkEntries.FirstOrDefault(w => w.Id == id);
            if (toRemove == null)
                return null;

            _context.WorkEntries.Remove(toRemove);

            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task Update(int id, WorkEntry newValue)
        {
            if (!this.Exists(id))
                throw new KeyNotFoundException();
            var entity = await _context.WorkEntries.FirstOrDefaultAsync(w => w.Id == id);
            entity = newValue;
            _context.WorkEntries.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
