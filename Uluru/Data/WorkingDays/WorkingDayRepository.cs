using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Data.WorkingDays
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        public Task Add(WorkingDayRepository toAdd)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkingDayRepository>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkingDayRepository> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WorkingDayRepository> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, WorkingDayRepository newValue)
        {
            throw new NotImplementedException();
        }
    }
}
