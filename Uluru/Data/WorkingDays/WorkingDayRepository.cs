using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.WorkingDays
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        private AppDbContext _context;

        public WorkingDayRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(WorkingDay toAdd)
        {
            _context.WorkingDays.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkingDay>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkingDay> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<WorkingAvailability>> GetAllOfUserAsync(int id)
        {
            var result = await _context.WorkingAvailabilities
                .Where(w => w.UserId == id)
                .ToListAsync();

            return result;
        }
        public Task<WorkingDay> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, WorkingDay newValue)
        {
            throw new NotImplementedException();
        }
    }
}
