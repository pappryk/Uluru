using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.WorkingDays
{
    public interface IWorkingDayRepository : IRepository<WorkingDay>
    {
        Task<IEnumerable<WorkingAvailability>> GetAllOfUserAsync(int id);
    }
}
