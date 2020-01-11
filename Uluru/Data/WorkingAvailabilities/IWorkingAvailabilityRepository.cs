using System.Collections.Generic;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.WorkingAvailabilities
{
    public interface IWorkingAvailabilityRepository : IRepository<WorkingAvailability>
    {
        Task<IEnumerable<WorkingAvailability>> GetAllOfUserAsync(int id);
        Task<IEnumerable<WorkingAvailability>> GetAllOfGroupAsync(int id);
    }
}
