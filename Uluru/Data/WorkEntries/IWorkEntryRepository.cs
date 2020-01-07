using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Models;

namespace Uluru.Data.WorkEntries
{
    public interface IWorkEntryRepository : IRepository<WorkEntry>
    {
        Task AddMany(IEnumerable<WorkEntry> workEntries);
    }
}
