using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class ScheduleChromosome : ChromosomeBase
    {
        private List<GAWorkEntry> _workEntries;
        public List<GAAvailability> Availabilities { get; private set; }

        public ScheduleChromosome(List<GAWorkEntry> workEntries, List<GAAvailability> availabilities) : base(workEntries.Count)
        {
            _workEntries = workEntries.DeepClone().Shuffled();
            Availabilities = availabilities.DeepClone().Shuffled();
            AssignAvailabilityToWorkEntry(); // tutaj b;ad
            var r = _workEntries.Select(w => w.Availability?.Id).ToList();
        }

        public void AssignAvailabilityToWorkEntry()
        {
            var availabilities = new List<GAAvailability>(
                Availabilities.Except(_workEntries.GetAvailabilities(),
                                       new GAAvailabilityEqualityComparer()).ToList()
            );

            foreach (var workEntry in _workEntries)
            {
                foreach (var availability in availabilities)
                {
                    if (workEntry.IsSatisfiedBy(availability))
                    {
                        workEntry.Availability = availability;
                        availabilities.Remove(availability);
                        break;
                    }
                }
            }

            ReplaceGenes(0, _workEntries.Select(w => new Gene(w)).ToArray());
        }

        public override IChromosome CreateNew()
        {
            return new ScheduleChromosome(_workEntries.DeepClone(), Availabilities.DeepClone());
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene();
        }

        public override IChromosome Clone()
        {
            var clone = base.Clone();

            return clone;
        }

        public void PrintWorkEntries()
        {
            foreach (var entry in _workEntries.OrderBy(w => w.Date))
            {
                Console.WriteLine(entry.Id.ToString() + ": " + entry.Start.ToString() + "  " + entry.End.ToString() + " | " + entry.Availability?.Id.ToString() + ": " + entry.Availability?.Start.ToString() + "  " + entry.Availability?.End.ToString());
            }

            var avs = _workEntries.Select(w => w.Availability);
            var availabilities = Availabilities.Except(avs);

            Console.WriteLine();

            foreach (var availability in availabilities.OrderBy(a => a.Date).ThenBy(a => a.End))
            {
                Console.WriteLine(availability.Id.ToString() + " | " + availability.Start.ToString() + "  " + availability.End.ToString());
            }
        }
    }

    public class ScheduleChromosomeEqualityComparer : IEqualityComparer<IChromosome>
    {
        public bool Equals([AllowNull] IChromosome x, [AllowNull] IChromosome y)
        {
            return Object.ReferenceEquals(x, y);
        }

        public int GetHashCode([DisallowNull] IChromosome obj)
        {
            return 0;
        }
    }
}