using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uluru.Scheduling
{
    public interface IScheduleGenerator
    {
        IEnumerable<GAWorkEntry> Generate(IList<GAWorkEntry> workEntries, IList<GAAvailability> availabilities);
    }
}
