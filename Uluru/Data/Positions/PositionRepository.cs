using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Data.Positions
{
    public class PositionRepository : IPositionRepository
    {
        private AppDbContext _context;
        public PositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Position toAdd)
        {
            _context.Positions.Add(toAdd);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.Positions.FirstOrDefault(w => w.Id == id);
            return result != null ? true : false;
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            var result = await _context.Positions
                .ToListAsync();

            return result;
        }

        public async Task<Position> GetById(int id)
        {
            var result = await _context.Positions
                .FirstOrDefaultAsync(w => w.Id == id);

            return result;
        }

        public async Task<Position> Remove(int id)
        {
            var toRemove = _context.Positions.FirstOrDefault(w => w.Id == id);
            if (toRemove == null)
                return null;

            _context.Positions.Remove(toRemove);

            await _context.SaveChangesAsync();
            return toRemove;
        }

        public async Task Update(int id, Position newValue)
        {
            if (!this.Exists(id))
                throw new KeyNotFoundException();
            var entity = await _context.Positions.FirstOrDefaultAsync(w => w.Id == id);
            entity = newValue;
            _context.Positions.Update(entity);
        }
    }
}
