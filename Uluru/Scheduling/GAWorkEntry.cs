using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Uluru.Models;

namespace Uluru.Scheduling
{
    [Serializable]
    public class GAWorkEntry
    {
        public int Id { get; set; }
        public GAAvailability Availability { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Date => Start.Date;

        public GAWorkEntry()
        {
        }

        public GAWorkEntry(WorkEntry workEntry)
        {
            Id = workEntry.Id;
            Start = workEntry.Start;
            End = workEntry.End;
        }
        public bool IsSatisfiedBy(GAAvailability availability)
        {
            if (Date == availability.Date && Start >= availability.Start && End <= availability.End)
                return true;
            return false;
        }
    }

    public class GAWorkEntryEqualityComparerWithAvailability : IEqualityComparer<GAWorkEntry>
    {
        public bool Equals(GAWorkEntry x, GAWorkEntry y)
        {
            //if (x != null && y == null)
            //    return false;
            //if (y != null && x == null)
            //    return false;

            return x?.Id == y?.Id &&
                    x?.Availability == y?.Availability;
        }

        public int GetHashCode([DisallowNull] GAWorkEntry obj)
        {
            return obj.Id;
        }
    }
}
