using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Uluru.Models;

namespace Uluru.Scheduling
{
    [Serializable]
    public class GAAvailability
    {
        public GAAvailability()
        {
        }

        public GAAvailability(WorkingAvailability workingAvailability)
        {
            Id = workingAvailability.Id;
            UserId = workingAvailability.UserId;
            Start = workingAvailability.Start;
            End = workingAvailability.End;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Date => Start.Date;
        public override bool Equals(object obj)
        {
            return ((GAAvailability)obj)?.Id == this?.Id;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class GAAvailabilityEqualityComparer : IEqualityComparer<GAAvailability>
    {
        public bool Equals(GAAvailability x, GAAvailability y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode([DisallowNull] GAAvailability obj)
        {
            return obj.Id;
        }
    }
}
