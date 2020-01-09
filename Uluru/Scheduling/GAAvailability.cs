using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Scheduling
{
    [Serializable]
    public class GAAvailability
    {
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
